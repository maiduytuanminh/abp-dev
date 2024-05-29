using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus;

namespace SmartSoftware.Blogging.Posts
{
    public class PostCacheInvalidator : ILocalEventHandler<PostChangedEvent>, ITransientDependency
    {
        protected IDistributedCache<List<PostCacheItem>> Cache { get; }

        public PostCacheInvalidator(IDistributedCache<List<PostCacheItem>> cache)
        {
            Cache = cache;
        }
        
        public virtual async Task HandleEventAsync(PostChangedEvent post)
        {
            await Cache.RemoveAsync(post.BlogId.ToString(), considerUow: true);
        }
    }
}