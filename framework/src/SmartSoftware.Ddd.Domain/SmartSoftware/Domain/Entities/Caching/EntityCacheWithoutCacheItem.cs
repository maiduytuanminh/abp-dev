using SmartSoftware.Caching;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Uow;

namespace SmartSoftware.Domain.Entities.Caching;

public class EntityCacheWithoutCacheItem<TEntity, TKey> :
    EntityCacheBase<TEntity, TEntity, TKey>
    where TEntity : Entity<TKey>
{
    public EntityCacheWithoutCacheItem(
        IReadOnlyRepository<TEntity, TKey> repository,
        IDistributedCache<TEntity, TKey> cache,
        IUnitOfWorkManager unitOfWorkManager)
        : base(repository, cache, unitOfWorkManager)
    {
    }

    protected override TEntity? MapToCacheItem(TEntity? entity)
    {
        return entity;
    }
}
