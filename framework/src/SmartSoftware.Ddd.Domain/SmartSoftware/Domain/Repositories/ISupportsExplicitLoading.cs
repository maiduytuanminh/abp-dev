using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Domain.Repositories;

public interface ISupportsExplicitLoading<TEntity>
    where TEntity : class, IEntity
{
    Task EnsureCollectionLoadedAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken)
        where TProperty : class;

    Task EnsurePropertyLoadedAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, TProperty?>> propertyExpression,
        CancellationToken cancellationToken)
        where TProperty : class;
}
