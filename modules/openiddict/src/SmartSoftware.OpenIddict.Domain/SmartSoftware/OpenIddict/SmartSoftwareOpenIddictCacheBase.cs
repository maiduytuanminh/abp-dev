using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSoftware.Caching;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictCacheBase<TEntity, TModel, TStore>
    where TModel : class
    where TEntity : class
{
    public ILogger<SmartSoftwareOpenIddictCacheBase<TEntity, TModel, TStore>> Logger { get; set; }

    protected IDistributedCache<TModel> Cache { get; }

    protected IDistributedCache<TModel[]> ArrayCache { get; }

    protected TStore Store { get; }

    protected SmartSoftwareOpenIddictCacheBase(IDistributedCache<TModel> cache, IDistributedCache<TModel[]> arrayCache, TStore store)
    {
        Cache = cache;
        ArrayCache = arrayCache;
        Store = store;

        Logger = NullLogger<SmartSoftwareOpenIddictCacheBase<TEntity, TModel, TStore>>.Instance;
    }
}
