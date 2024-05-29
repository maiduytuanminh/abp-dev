﻿using System;
using System.Threading.Tasks;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Domain.Repositories.MemoryDb;

public interface IMemoryDbRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    [Obsolete("Use GetDatabaseAsync() method.")]
    IMemoryDatabase Database { get; }

    [Obsolete("Use GetCollectionAsync() method.")]
    IMemoryDatabaseCollection<TEntity> Collection { get; }

    Task<IMemoryDatabase> GetDatabaseAsync();

    Task<IMemoryDatabaseCollection<TEntity>> GetCollectionAsync();
}

public interface IMemoryDbRepository<TEntity, TKey> : IMemoryDbRepository<TEntity>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{

}
