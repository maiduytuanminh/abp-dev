using System;
using SmartSoftware.Caching;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Uow;

namespace SmartSoftware.Domain.Entities.Caching;

public class EntityCacheWithObjectMapper<TEntity, TEntityCacheItem, TKey> :
    EntityCacheBase<TEntity, TEntityCacheItem, TKey>
    where TEntity : Entity<TKey>
    where TEntityCacheItem : class
{
    protected IObjectMapper ObjectMapper { get; }

    public EntityCacheWithObjectMapper(
        IReadOnlyRepository<TEntity, TKey> repository,
        IDistributedCache<TEntityCacheItem, TKey> cache,
        IUnitOfWorkManager unitOfWorkManager,
        IObjectMapper objectMapper)
        : base(repository, cache, unitOfWorkManager)
    {
        ObjectMapper = objectMapper;
    }

    protected override TEntityCacheItem? MapToCacheItem(TEntity? entity)
    {
        if (entity == null)
        {
            return null;
        }

        if (typeof(TEntity) == typeof(TEntityCacheItem))
        {
            return entity.As<TEntityCacheItem>();
        }

        return ObjectMapper.Map<TEntity, TEntityCacheItem>(entity);
    }
}
