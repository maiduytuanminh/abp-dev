using System;
using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;
using SmartSoftware.EventBus.Local;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TenantManagement;

[LocalEventHandlerOrder(-1)]
public class TenantConfigurationCacheItemInvalidator :
    ILocalEventHandler<EntityChangedEventData<Tenant>>,
    ILocalEventHandler<TenantChangedEvent>,
    ITransientDependency
{
    protected IDistributedCache<TenantConfigurationCacheItem> Cache { get; }

    public TenantConfigurationCacheItemInvalidator(IDistributedCache<TenantConfigurationCacheItem> cache)
    {
        Cache = cache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<Tenant> eventData)
    {
        if (eventData is EntityCreatedEventData<Tenant>)
        {
            return;
        }

        await ClearCacheAsync(eventData.Entity.Id, eventData.Entity.NormalizedName);
    }

    public virtual async Task HandleEventAsync(TenantChangedEvent eventData)
    {
        await ClearCacheAsync(eventData.Id, eventData.NormalizedName);
    }

    protected virtual async Task ClearCacheAsync(Guid? id, string normalizedName)
    {
        await Cache.RemoveManyAsync(
            new[]
            {
                TenantConfigurationCacheItem.CalculateCacheKey(id, null),
                TenantConfigurationCacheItem.CalculateCacheKey(null, normalizedName),
                TenantConfigurationCacheItem.CalculateCacheKey(id, normalizedName),
            }, considerUow: true);
    }
}