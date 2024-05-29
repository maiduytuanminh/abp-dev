using System;
using JetBrains.Annotations;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public interface ISmartSoftwareDbContextRegistrationOptionsBuilder : ISmartSoftwareCommonDbContextRegistrationOptionsBuilder
{
    void Entity<TEntity>([NotNull] Action<SmartSoftwareEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity;
}
