using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public class SmartSoftwareEntityOptions<TEntity>
    where TEntity : IEntity
{
    public static SmartSoftwareEntityOptions<TEntity> Empty { get; } = new SmartSoftwareEntityOptions<TEntity>();

    public Func<IQueryable<TEntity>, IQueryable<TEntity>>? DefaultWithDetailsFunc { get; set; }
}

public class SmartSoftwareEntityOptions
{
    private readonly IDictionary<Type, object> _options;

    public SmartSoftwareEntityOptions()
    {
        _options = new Dictionary<Type, object>();
    }

    public SmartSoftwareEntityOptions<TEntity>? GetOrNull<TEntity>()
        where TEntity : IEntity
    {
        return _options.GetOrDefault(typeof(TEntity)) as SmartSoftwareEntityOptions<TEntity>;
    }

    public void Entity<TEntity>([NotNull] Action<SmartSoftwareEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity
    {
        Check.NotNull(optionsAction, nameof(optionsAction));

        optionsAction(
            (_options.GetOrAdd(
                typeof(TEntity),
                () => new SmartSoftwareEntityOptions<TEntity>()
            ) as SmartSoftwareEntityOptions<TEntity>)!
        );
    }
}
