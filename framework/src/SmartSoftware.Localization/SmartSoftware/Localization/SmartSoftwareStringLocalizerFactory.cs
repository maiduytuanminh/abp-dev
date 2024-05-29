using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SmartSoftware.Localization.External;
using SmartSoftware.Threading;

namespace SmartSoftware.Localization;

public class SmartSoftwareStringLocalizerFactory : IStringLocalizerFactory, ISmartSoftwareStringLocalizerFactory
{
    protected internal SmartSoftwareLocalizationOptions SmartSoftwareLocalizationOptions { get; }
    protected ResourceManagerStringLocalizerFactory InnerFactory { get; }
    protected IServiceProvider ServiceProvider { get; }
    protected IExternalLocalizationStore ExternalLocalizationStore { get; }
    protected ConcurrentDictionary<string, StringLocalizerCacheItem> LocalizerCache { get; }
    protected SemaphoreSlim LocalizerCacheSemaphore { get; } = new(1, 1);

    public SmartSoftwareStringLocalizerFactory(
        ResourceManagerStringLocalizerFactory innerFactory,
        IOptions<SmartSoftwareLocalizationOptions> ssLocalizationOptions,
        IServiceProvider serviceProvider,
        IExternalLocalizationStore externalLocalizationStore)
    {
        InnerFactory = innerFactory;
        ServiceProvider = serviceProvider;
        ExternalLocalizationStore = externalLocalizationStore;
        SmartSoftwareLocalizationOptions = ssLocalizationOptions.Value;

        LocalizerCache = new ConcurrentDictionary<string, StringLocalizerCacheItem>();
    }

    public virtual IStringLocalizer Create(Type resourceType)
    {
        return Create(resourceType, lockCache: true);
    }
    
    private IStringLocalizer Create(Type resourceType, bool lockCache)
    {
        var resource = SmartSoftwareLocalizationOptions.Resources.GetOrNull(resourceType);
        if (resource == null)
        {
            return InnerFactory.Create(resourceType);
        }

        return CreateInternal(resource.ResourceName, resource, lockCache);
    }
    
    public IStringLocalizer? CreateByResourceNameOrNull(string resourceName)
    {
        return CreateByResourceNameOrNullInternal(resourceName, lockCache: true);
    }
    
    private IStringLocalizer? CreateByResourceNameOrNullInternal(
        string resourceName,
        bool lockCache)
    {
        var resource = SmartSoftwareLocalizationOptions.Resources.GetOrDefault(resourceName);
        if (resource == null)
        {
            resource = ExternalLocalizationStore.GetResourceOrNull(resourceName);
            if (resource == null)
            {
                return null;
            }
        }

        return CreateInternal(resourceName, resource, lockCache);
    }

    public Task<IStringLocalizer?> CreateByResourceNameOrNullAsync(string resourceName)
    {
        return CreateByResourceNameOrNullInternalAsync(resourceName, lockCache: true);
    }
    
    private async Task<IStringLocalizer?> CreateByResourceNameOrNullInternalAsync(
        string resourceName,
        bool lockCache)
    {
        var resource = SmartSoftwareLocalizationOptions.Resources.GetOrDefault(resourceName);
        if (resource == null)
        {
            resource = await ExternalLocalizationStore.GetResourceOrNullAsync(resourceName);
            if (resource == null)
            {
                return null;
            }
        }

        return await CreateInternalAsync(resourceName, resource, lockCache);
    }

    private IStringLocalizer CreateInternal(
        string resourceName,
        LocalizationResourceBase resource,
        bool lockCache)
    {
        if (LocalizerCache.TryGetValue(resourceName, out var cacheItem))
        {
            return cacheItem.Localizer;
        }

        IStringLocalizer GetOrCreateLocalizer()
        {
            // Double check
            if (LocalizerCache.TryGetValue(resourceName, out var cacheItem2))
            {
                return cacheItem2.Localizer;
            }

            return LocalizerCache.GetOrAdd(
                resourceName,
                _ => CreateStringLocalizerCacheItem(resource)
            ).Localizer;
        }

        if (lockCache)
        {
            using (LocalizerCacheSemaphore.Lock())
            {
                return GetOrCreateLocalizer();
            }
        }
        else
        {
            return GetOrCreateLocalizer();
        }
    }
    
    private async Task<IStringLocalizer> CreateInternalAsync(
        string resourceName,
        LocalizationResourceBase resource,
        bool lockCache)
    {
        if (LocalizerCache.TryGetValue(resourceName, out var cacheItem))
        {
            return cacheItem.Localizer;
        }

        async Task<IStringLocalizer> GetOrCreateLocalizerAsync()
        {
            // Double check
            if (LocalizerCache.TryGetValue(resourceName, out var cacheItem2))
            {
                return cacheItem2.Localizer;
            }

            var newCacheItem = await CreateStringLocalizerCacheItemAsync(resource);
            LocalizerCache[resourceName] = newCacheItem;
            return newCacheItem.Localizer;
        }

        if (lockCache)
        {
            using (await LocalizerCacheSemaphore.LockAsync())
            {
                return await GetOrCreateLocalizerAsync();
            }
        }
        else
        {
            return await GetOrCreateLocalizerAsync();
        }
    }

    private StringLocalizerCacheItem CreateStringLocalizerCacheItem(LocalizationResourceBase resource)
    {
        foreach (var globalContributorType in SmartSoftwareLocalizationOptions.GlobalContributors)
        {
            resource.Contributors.Add(
                Activator
                    .CreateInstance(globalContributorType)!
                    .As<ILocalizationResourceContributor>()
            );
        }

        var context = new LocalizationResourceInitializationContext(resource, ServiceProvider);

        foreach (var contributor in resource.Contributors)
        {
            contributor.Initialize(context);
        }

        return new StringLocalizerCacheItem(
            new SmartSoftwareDictionaryBasedStringLocalizer(
                resource,
                resource
                    .BaseResourceNames
                    .Select(x => CreateByResourceNameOrNullInternal(x, lockCache: false))
                    .Where(x => x != null)
                    .ToList()!,
                SmartSoftwareLocalizationOptions
            )
        );
    }
    
    private async Task<StringLocalizerCacheItem> CreateStringLocalizerCacheItemAsync(LocalizationResourceBase resource)
    {
        foreach (var globalContributorType in SmartSoftwareLocalizationOptions.GlobalContributors)
        {
            resource.Contributors.Add(
                Activator
                    .CreateInstance(globalContributorType)!
                    .As<ILocalizationResourceContributor>()
            );
        }

        var context = new LocalizationResourceInitializationContext(resource, ServiceProvider);

        foreach (var contributor in resource.Contributors)
        {
            contributor.Initialize(context);
        }
        
        var baseLocalizers = new List<IStringLocalizer>();
        
        foreach (var baseResourceName in resource.BaseResourceNames)
        {
            var baseLocalizer = await CreateByResourceNameOrNullInternalAsync(baseResourceName, lockCache: false);
            if (baseLocalizer != null)
            {
                baseLocalizers.Add(baseLocalizer);
            }
        }

        return new StringLocalizerCacheItem(
            new SmartSoftwareDictionaryBasedStringLocalizer(
                resource,
                baseLocalizers,
                SmartSoftwareLocalizationOptions
            )
        );
    }

    public virtual IStringLocalizer Create(string baseName, string location)
    {
        return InnerFactory.Create(baseName, location);
    }

    internal static void Replace(IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, SmartSoftwareStringLocalizerFactory>());
        services.AddSingleton<ResourceManagerStringLocalizerFactory>();
    }

    protected class StringLocalizerCacheItem
    {
        public SmartSoftwareDictionaryBasedStringLocalizer Localizer { get; }

        public StringLocalizerCacheItem(SmartSoftwareDictionaryBasedStringLocalizer localizer)
        {
            Localizer = localizer;
        }
    }

    public IStringLocalizer? CreateDefaultOrNull()
    {
        if (SmartSoftwareLocalizationOptions.DefaultResourceType == null)
        {
            return null;
        }

        return Create(SmartSoftwareLocalizationOptions.DefaultResourceType);
    }
}