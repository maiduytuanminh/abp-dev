using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DistributedLocking;
using SmartSoftware.Features;
using SmartSoftware.Json.SystemTextJson.Modifiers;
using SmartSoftware.Threading;
using SmartSoftware.Uow;

namespace SmartSoftware.FeatureManagement;

public class StaticFeatureSaver : IStaticFeatureSaver, ITransientDependency
{
    protected IStaticFeatureDefinitionStore StaticStore { get; }
    protected IFeatureGroupDefinitionRecordRepository FeatureGroupRepository { get; }
    protected IFeatureDefinitionRecordRepository FeatureRepository { get; }
    protected IFeatureDefinitionSerializer FeatureSerializer { get; }
    protected IDistributedCache Cache { get; }
    protected IApplicationInfoAccessor ApplicationInfoAccessor { get; }
    protected ISmartSoftwareDistributedLock DistributedLock { get; }
    protected SmartSoftwareFeatureOptions FeatureOptions { get; }
    protected ICancellationTokenProvider CancellationTokenProvider { get; }
    protected SmartSoftwareDistributedCacheOptions CacheOptions { get; }
    
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public StaticFeatureSaver(
        IStaticFeatureDefinitionStore staticStore,
        IFeatureGroupDefinitionRecordRepository featureGroupRepository,
        IFeatureDefinitionRecordRepository featureRepository,
        IFeatureDefinitionSerializer featureSerializer,
        IDistributedCache cache,
        IOptions<SmartSoftwareDistributedCacheOptions> cacheOptions,
        IApplicationInfoAccessor applicationInfoAccessor,
        ISmartSoftwareDistributedLock distributedLock,
        IOptions<SmartSoftwareFeatureOptions> featureManagementOptions,
        ICancellationTokenProvider cancellationTokenProvider,
        IUnitOfWorkManager unitOfWorkManager)
    {
        StaticStore = staticStore;
        FeatureGroupRepository = featureGroupRepository;
        FeatureRepository = featureRepository;
        FeatureSerializer = featureSerializer;
        Cache = cache;
        ApplicationInfoAccessor = applicationInfoAccessor;
        DistributedLock = distributedLock;
        CancellationTokenProvider = cancellationTokenProvider;
        UnitOfWorkManager = unitOfWorkManager;
        FeatureOptions = featureManagementOptions.Value;
        CacheOptions = cacheOptions.Value;
    }

    public async Task SaveAsync()
    {
        await using var applicationLockHandle = await DistributedLock.TryAcquireAsync(
            GetApplicationDistributedLockKey()
        );

        if (applicationLockHandle == null)
        {
            /* Another application instance is already doing it */
            return;
        }

        /* NOTE: This can be further optimized by using 4 cache values for:
         * Groups, features, deleted groups and deleted features.
         * But the code would be more complex. This is enough for now.
         */

        var cacheKey = GetApplicationHashCacheKey();
        var cachedHash = await Cache.GetStringAsync(cacheKey, CancellationTokenProvider.Token);

        var (featureGroupRecords, featureRecords) = await FeatureSerializer.SerializeAsync(
            await StaticStore.GetGroupsAsync()
        );

        var currentHash = CalculateHash(
            featureGroupRecords,
            featureRecords,
            FeatureOptions.DeletedFeatureGroups,
            FeatureOptions.DeletedFeatures
        );

        if (cachedHash == currentHash)
        {
            return;
        }

        await using (var commonLockHandle = await DistributedLock.TryAcquireAsync(
                         GetCommonDistributedLockKey(),
                         TimeSpan.FromMinutes(5)))
        {
            if (commonLockHandle == null)
            {
                /* It will re-try */
                throw new SmartSoftwareException("Could not acquire distributed lock for saving static features!");
            }

            using (var unitOfWork = UnitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
            {
                try
                {
                    var hasChangesInGroups = await UpdateChangedFeatureGroupsAsync(featureGroupRecords);
                    var hasChangesInFeatures = await UpdateChangedFeaturesAsync(featureRecords);

                    if (hasChangesInGroups ||hasChangesInFeatures)
                    {
                        await Cache.SetStringAsync(
                            GetCommonStampCacheKey(),
                            Guid.NewGuid().ToString(),
                            new DistributedCacheEntryOptions {
                                SlidingExpiration = TimeSpan.FromDays(30) //TODO: Make it configurable?
                            },
                            CancellationTokenProvider.Token
                        );
                    }
                }
                catch
                {
                    try
                    {
                        await unitOfWork.RollbackAsync();
                    }
                    catch
                    {
                        /* ignored */
                    }
                    
                    throw;
                }

                await unitOfWork.CompleteAsync();
            }
        }

        await Cache.SetStringAsync(
            cacheKey,
            currentHash,
            new DistributedCacheEntryOptions {
                SlidingExpiration = TimeSpan.FromDays(30) //TODO: Make it configurable?
            },
            CancellationTokenProvider.Token
        );
    }

    private async Task<bool> UpdateChangedFeatureGroupsAsync(
        IEnumerable<FeatureGroupDefinitionRecord> featureGroupRecords)
    {
        var newRecords = new List<FeatureGroupDefinitionRecord>();
        var changedRecords = new List<FeatureGroupDefinitionRecord>();

        var featureGroupRecordsInDatabase = (await FeatureGroupRepository.GetListAsync())
            .ToDictionary(x => x.Name);

        foreach (var featureGroupRecord in featureGroupRecords)
        {
            var featureGroupRecordInDatabase = featureGroupRecordsInDatabase.GetOrDefault(featureGroupRecord.Name);
            if (featureGroupRecordInDatabase == null)
            {
                /* New group */
                newRecords.Add(featureGroupRecord);
                continue;
            }

            if (featureGroupRecord.HasSameData(featureGroupRecordInDatabase))
            {
                /* Not changed */
                continue;
            }

            /* Changed */
            featureGroupRecordInDatabase.Patch(featureGroupRecord);
            changedRecords.Add(featureGroupRecordInDatabase);
        }

        /* Deleted */
        var deletedRecords = FeatureOptions.DeletedFeatureGroups.Any()
            ? featureGroupRecordsInDatabase.Values
                .Where(x => FeatureOptions.DeletedFeatureGroups.Contains(x.Name))
                .ToArray()
            : Array.Empty<FeatureGroupDefinitionRecord>();

        if (newRecords.Any())
        {
            await FeatureGroupRepository.InsertManyAsync(newRecords);
        }

        if (changedRecords.Any())
        {
            await FeatureGroupRepository.UpdateManyAsync(changedRecords);
        }

        if (deletedRecords.Any())
        {
            await FeatureGroupRepository.DeleteManyAsync(deletedRecords);
        }

        return newRecords.Any() || changedRecords.Any() || deletedRecords.Any();
    }

    private async Task<bool> UpdateChangedFeaturesAsync(
        IEnumerable<FeatureDefinitionRecord> featureRecords)
    {
        var newRecords = new List<FeatureDefinitionRecord>();
        var changedRecords = new List<FeatureDefinitionRecord>();

        var featureRecordsInDatabase = (await FeatureRepository.GetListAsync())
            .ToDictionary(x => x.Name);

        foreach (var featureRecord in featureRecords)
        {
            var featureRecordInDatabase = featureRecordsInDatabase.GetOrDefault(featureRecord.Name);
            if (featureRecordInDatabase == null)
            {
                /* New group */
                newRecords.Add(featureRecord);
                continue;
            }

            if (featureRecord.HasSameData(featureRecordInDatabase))
            {
                /* Not changed */
                continue;
            }

            /* Changed */
            featureRecordInDatabase.Patch(featureRecord);
            changedRecords.Add(featureRecordInDatabase);
        }

        /* Deleted */
        var deletedRecords = new List<FeatureDefinitionRecord>();

        if (FeatureOptions.DeletedFeatures.Any())
        {
            deletedRecords.AddRange(
                featureRecordsInDatabase.Values
                    .Where(x => FeatureOptions.DeletedFeatures.Contains(x.Name))
            );
        }

        if (FeatureOptions.DeletedFeatureGroups.Any())
        {
            deletedRecords.AddIfNotContains(
                featureRecordsInDatabase.Values
                    .Where(x => FeatureOptions.DeletedFeatureGroups.Contains(x.GroupName))
            );
        }

        if (newRecords.Any())
        {
            await FeatureRepository.InsertManyAsync(newRecords);
        }

        if (changedRecords.Any())
        {
            await FeatureRepository.UpdateManyAsync(changedRecords);
        }

        if (deletedRecords.Any())
        {
            await FeatureRepository.DeleteManyAsync(deletedRecords);
        }

        return newRecords.Any() || changedRecords.Any() || deletedRecords.Any();
    }

    private string GetApplicationDistributedLockKey()
    {
        return $"{CacheOptions.KeyPrefix}_{ApplicationInfoAccessor.ApplicationName}_SmartSoftwareFeatureUpdateLock";
    }

    private string GetCommonDistributedLockKey()
    {
        return $"{CacheOptions.KeyPrefix}_Common_SmartSoftwareFeatureUpdateLock";
    }

    private string GetApplicationHashCacheKey()
    {
        return $"{CacheOptions.KeyPrefix}_{ApplicationInfoAccessor.ApplicationName}_SmartSoftwareFeaturesHash";
    }

    private string GetCommonStampCacheKey()
    {
        return $"{CacheOptions.KeyPrefix}_SmartSoftwareInMemoryFeatureCacheStamp";
    }

    private static string CalculateHash(
        FeatureGroupDefinitionRecord[] featureGroupRecords,
        FeatureDefinitionRecord[] featureRecords,
        IEnumerable<string> deletedFeatureGroups,
        IEnumerable<string> deletedFeatures)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    new SmartSoftwareIgnorePropertiesModifiers<FeatureGroupDefinitionRecord, Guid>().CreateModifyAction(x => x.Id),
                    new SmartSoftwareIgnorePropertiesModifiers<FeatureDefinitionRecord, Guid>().CreateModifyAction(x => x.Id)
                }
            }
        };

        var stringBuilder = new StringBuilder();

        stringBuilder.Append("FeatureGroupRecords:");
        stringBuilder.AppendLine(JsonSerializer.Serialize(featureGroupRecords, jsonSerializerOptions));

        stringBuilder.Append("FeatureRecords:");
        stringBuilder.AppendLine(JsonSerializer.Serialize(featureRecords, jsonSerializerOptions));

        stringBuilder.Append("DeletedFeatureGroups:");
        stringBuilder.AppendLine(deletedFeatureGroups.JoinAsString(","));

        stringBuilder.Append("DeletedFeature:");
        stringBuilder.Append(deletedFeatures.JoinAsString(","));

        return stringBuilder
            .ToString()
            .ToMd5();
    }
}
