using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DistributedLocking;
using SmartSoftware.Json.SystemTextJson.Modifiers;
using SmartSoftware.Threading;
using SmartSoftware.Uow;

namespace SmartSoftware.PermissionManagement;

public class StaticPermissionSaver : IStaticPermissionSaver, ITransientDependency
{
    protected IStaticPermissionDefinitionStore StaticStore { get; }
    protected IPermissionGroupDefinitionRecordRepository PermissionGroupRepository { get; }
    protected IPermissionDefinitionRecordRepository PermissionRepository { get; }
    protected IPermissionDefinitionSerializer PermissionSerializer { get; }
    protected IDistributedCache Cache { get; }
    protected IApplicationInfoAccessor ApplicationInfoAccessor { get; }
    protected ISmartSoftwareDistributedLock DistributedLock { get; }
    protected SmartSoftwarePermissionOptions PermissionOptions { get; }
    protected ICancellationTokenProvider CancellationTokenProvider { get; }
    protected SmartSoftwareDistributedCacheOptions CacheOptions { get; }

    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public StaticPermissionSaver(
        IStaticPermissionDefinitionStore staticStore,
        IPermissionGroupDefinitionRecordRepository permissionGroupRepository,
        IPermissionDefinitionRecordRepository permissionRepository,
        IPermissionDefinitionSerializer permissionSerializer,
        IDistributedCache cache,
        IOptions<SmartSoftwareDistributedCacheOptions> cacheOptions,
        IApplicationInfoAccessor applicationInfoAccessor,
        ISmartSoftwareDistributedLock distributedLock,
        IOptions<SmartSoftwarePermissionOptions> permissionOptions,
        ICancellationTokenProvider cancellationTokenProvider,
        IUnitOfWorkManager unitOfWorkManager)
    {
        UnitOfWorkManager = unitOfWorkManager;
        StaticStore = staticStore;
        PermissionGroupRepository = permissionGroupRepository;
        PermissionRepository = permissionRepository;
        PermissionSerializer = permissionSerializer;
        Cache = cache;
        ApplicationInfoAccessor = applicationInfoAccessor;
        DistributedLock = distributedLock;
        CancellationTokenProvider = cancellationTokenProvider;
        PermissionOptions = permissionOptions.Value;
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
         * Groups, permissions, deleted groups and deleted permissions.
         * But the code would be more complex. This is enough for now.
         */

        var cacheKey = GetApplicationHashCacheKey();
        var cachedHash = await Cache.GetStringAsync(cacheKey, CancellationTokenProvider.Token);

        var (permissionGroupRecords, permissionRecords) = await PermissionSerializer.SerializeAsync(
            await StaticStore.GetGroupsAsync()
        );

        var currentHash = CalculateHash(
            permissionGroupRecords,
            permissionRecords,
            PermissionOptions.DeletedPermissionGroups,
            PermissionOptions.DeletedPermissions
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
                throw new SmartSoftwareException("Could not acquire distributed lock for saving static permissions!");
            }

            using (var unitOfWork = UnitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
            {
                try
                {
                    var hasChangesInGroups = await UpdateChangedPermissionGroupsAsync(permissionGroupRecords);
                    var hasChangesInPermissions = await UpdateChangedPermissionsAsync(permissionRecords);

                    if (hasChangesInGroups || hasChangesInPermissions)
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

    private async Task<bool> UpdateChangedPermissionGroupsAsync(
        IEnumerable<PermissionGroupDefinitionRecord> permissionGroupRecords)
    {
        var newRecords = new List<PermissionGroupDefinitionRecord>();
        var changedRecords = new List<PermissionGroupDefinitionRecord>();

        var permissionGroupRecordsInDatabase = (await PermissionGroupRepository.GetListAsync())
            .ToDictionary(x => x.Name);

        foreach (var permissionGroupRecord in permissionGroupRecords)
        {
            var permissionGroupRecordInDatabase =
                permissionGroupRecordsInDatabase.GetOrDefault(permissionGroupRecord.Name);
            if (permissionGroupRecordInDatabase == null)
            {
                /* New group */
                newRecords.Add(permissionGroupRecord);
                continue;
            }

            if (permissionGroupRecord.HasSameData(permissionGroupRecordInDatabase))
            {
                /* Not changed */
                continue;
            }

            /* Changed */
            permissionGroupRecordInDatabase.Patch(permissionGroupRecord);
            changedRecords.Add(permissionGroupRecordInDatabase);
        }

        /* Deleted */
        var deletedRecords = PermissionOptions.DeletedPermissionGroups.Any()
            ? permissionGroupRecordsInDatabase.Values
                .Where(x => PermissionOptions.DeletedPermissionGroups.Contains(x.Name))
                .ToArray()
            : Array.Empty<PermissionGroupDefinitionRecord>();

        if (newRecords.Any())
        {
            await PermissionGroupRepository.InsertManyAsync(newRecords);
        }

        if (changedRecords.Any())
        {
            await PermissionGroupRepository.UpdateManyAsync(changedRecords);
        }

        if (deletedRecords.Any())
        {
            await PermissionGroupRepository.DeleteManyAsync(deletedRecords);
        }

        return newRecords.Any() || changedRecords.Any() || deletedRecords.Any();
    }

    private async Task<bool> UpdateChangedPermissionsAsync(
        IEnumerable<PermissionDefinitionRecord> permissionRecords)
    {
        var newRecords = new List<PermissionDefinitionRecord>();
        var changedRecords = new List<PermissionDefinitionRecord>();

        var permissionRecordsInDatabase = (await PermissionRepository.GetListAsync())
            .ToDictionary(x => x.Name);

        foreach (var permissionRecord in permissionRecords)
        {
            var permissionRecordInDatabase = permissionRecordsInDatabase.GetOrDefault(permissionRecord.Name);
            if (permissionRecordInDatabase == null)
            {
                /* New group */
                newRecords.Add(permissionRecord);
                continue;
            }

            if (permissionRecord.HasSameData(permissionRecordInDatabase))
            {
                /* Not changed */
                continue;
            }

            /* Changed */
            permissionRecordInDatabase.Patch(permissionRecord);
            changedRecords.Add(permissionRecordInDatabase);
        }

        /* Deleted */
        var deletedRecords = new List<PermissionDefinitionRecord>();

        if (PermissionOptions.DeletedPermissions.Any())
        {
            deletedRecords.AddRange(
                permissionRecordsInDatabase.Values
                    .Where(x => PermissionOptions.DeletedPermissions.Contains(x.Name))
            );
        }

        if (PermissionOptions.DeletedPermissionGroups.Any())
        {
            deletedRecords.AddIfNotContains(
                permissionRecordsInDatabase.Values
                    .Where(x => PermissionOptions.DeletedPermissionGroups.Contains(x.GroupName))
            );
        }

        if (newRecords.Any())
        {
            await PermissionRepository.InsertManyAsync(newRecords);
        }

        if (changedRecords.Any())
        {
            await PermissionRepository.UpdateManyAsync(changedRecords);
        }

        if (deletedRecords.Any())
        {
            await PermissionRepository.DeleteManyAsync(deletedRecords);
        }

        return newRecords.Any() || changedRecords.Any() || deletedRecords.Any();
    }

    private string GetApplicationDistributedLockKey()
    {
        return $"{CacheOptions.KeyPrefix}_{ApplicationInfoAccessor.ApplicationName}_SmartSoftwarePermissionUpdateLock";
    }

    private string GetCommonDistributedLockKey()
    {
        return $"{CacheOptions.KeyPrefix}_Common_SmartSoftwarePermissionUpdateLock";
    }

    private string GetApplicationHashCacheKey()
    {
        return $"{CacheOptions.KeyPrefix}_{ApplicationInfoAccessor.ApplicationName}_SmartSoftwarePermissionsHash";
    }

    private string GetCommonStampCacheKey()
    {
        return $"{CacheOptions.KeyPrefix}_SmartSoftwareInMemoryPermissionCacheStamp";
    }

    private static string CalculateHash(
        PermissionGroupDefinitionRecord[] permissionGroupRecords,
        PermissionDefinitionRecord[] permissionRecords,
        IEnumerable<string> deletedPermissionGroups,
        IEnumerable<string> deletedPermissions)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    new SmartSoftwareIgnorePropertiesModifiers<PermissionGroupDefinitionRecord, Guid>().CreateModifyAction(x => x.Id),
                    new SmartSoftwareIgnorePropertiesModifiers<PermissionDefinitionRecord, Guid>().CreateModifyAction(x => x.Id)
                }
            }
        };

        var stringBuilder = new StringBuilder();

        stringBuilder.Append("PermissionGroupRecords:");
        stringBuilder.AppendLine(JsonSerializer.Serialize(permissionGroupRecords, jsonSerializerOptions));

        stringBuilder.Append("PermissionRecords:");
        stringBuilder.AppendLine(JsonSerializer.Serialize(permissionRecords, jsonSerializerOptions));

        stringBuilder.Append("DeletedPermissionGroups:");
        stringBuilder.AppendLine(deletedPermissionGroups.JoinAsString(","));

        stringBuilder.Append("DeletedPermission:");
        stringBuilder.Append(deletedPermissions.JoinAsString(","));

        return stringBuilder
            .ToString()
            .ToMd5();
    }
}