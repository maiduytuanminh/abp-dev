using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;

namespace SmartSoftware.SettingManagement;

public class SettingCacheItemInvalidator : ILocalEventHandler<EntityChangedEventData<Setting>>, ITransientDependency
{
    protected IDistributedCache<SettingCacheItem> Cache { get; }

    public SettingCacheItemInvalidator(IDistributedCache<SettingCacheItem> cache)
    {
        Cache = cache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<Setting> eventData)
    {
        var cacheKey = CalculateCacheKey(
            eventData.Entity.Name,
            eventData.Entity.ProviderName,
            eventData.Entity.ProviderKey
        );

        await Cache.RemoveAsync(cacheKey, considerUow: true);
    }

    protected virtual string CalculateCacheKey(string name, string providerName, string providerKey)
    {
        return SettingCacheItem.CalculateCacheKey(name, providerName, providerKey);
    }
}
