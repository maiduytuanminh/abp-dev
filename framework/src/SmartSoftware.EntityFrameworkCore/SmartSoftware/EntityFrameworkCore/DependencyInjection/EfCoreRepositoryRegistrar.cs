using System;
using System.Collections.Generic;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public class EfCoreRepositoryRegistrar : RepositoryRegistrarBase<SmartSoftwareDbContextRegistrationOptions>
{
    public EfCoreRepositoryRegistrar(SmartSoftwareDbContextRegistrationOptions options)
        : base(options)
    {

    }

    protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return DbContextHelper.GetEntityTypes(dbContextType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType)
    {
        return typeof(EfCoreRepository<,>).MakeGenericType(dbContextType, entityType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
    {
        return typeof(EfCoreRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
    }
}
