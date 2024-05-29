using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;

namespace SmartSoftware.FeatureManagement;

public class FeatureValueCacheItemInvalidator :
    ILocalEventHandler<EntityChangedEventData<FeatureValue>>,
    ITransientDependency
{
    protected IDistributedCache<FeatureValueCacheItem> Cache { get; }

    public FeatureValueCacheItemInvalidator(IDistributedCache<FeatureValueCacheItem> cache)
    {
        Cache = cache;
    }

    public virtual async Task HandleEventAsync(EntityChangedEventData<FeatureValue> eventData)
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
        return FeatureValueCacheItem.CalculateCacheKey(name, providerName, providerKey);
    }
}
