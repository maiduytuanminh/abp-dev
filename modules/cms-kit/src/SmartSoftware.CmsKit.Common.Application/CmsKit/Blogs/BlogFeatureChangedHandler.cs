using System;
using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;

namespace SmartSoftware.CmsKit.Blogs;

public class BlogFeatureChangedHandler :
    ILocalEventHandler<EntityCreatedEventData<BlogFeature>>,
    ILocalEventHandler<EntityUpdatedEventData<BlogFeature>>,
    ITransientDependency
{
    protected IDistributedCache<BlogFeatureCacheItem, BlogFeatureCacheKey> Cache { get; }

    public BlogFeatureChangedHandler(
        IDistributedCache<BlogFeatureCacheItem, BlogFeatureCacheKey> cache)
    {
        Cache = cache;
    }

    public Task RemoveFromCacheAsync(Guid blogId, string featureName)
    {
        return Cache.RemoveAsync(new BlogFeatureCacheKey(blogId, featureName));
    }

    public Task HandleEventAsync(EntityCreatedEventData<BlogFeature> eventData)
    {
        return RemoveFromCacheAsync(eventData.Entity.BlogId, eventData.Entity.FeatureName);
    }

    public Task HandleEventAsync(EntityUpdatedEventData<BlogFeature> eventData)
    {
        return RemoveFromCacheAsync(eventData.Entity.BlogId, eventData.Entity.FeatureName);
    }
}
