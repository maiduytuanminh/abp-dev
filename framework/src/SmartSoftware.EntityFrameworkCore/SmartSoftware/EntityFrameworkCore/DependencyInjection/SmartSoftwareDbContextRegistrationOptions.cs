using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public class SmartSoftwareDbContextRegistrationOptions : SmartSoftwareCommonDbContextRegistrationOptions, ISmartSoftwareDbContextRegistrationOptionsBuilder
{
    public Dictionary<Type, object> SmartSoftwareEntityOptions { get; }

    public SmartSoftwareDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
        SmartSoftwareEntityOptions = new Dictionary<Type, object>();
    }

    public void Entity<TEntity>(Action<SmartSoftwareEntityOptions<TEntity>> optionsAction) where TEntity : IEntity
    {
        Services.Configure<SmartSoftwareEntityOptions>(options =>
        {
            options.Entity(optionsAction);
        });
    }
}
