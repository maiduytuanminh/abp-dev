using System;
using System.Collections.Generic;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.MemoryDb;

namespace SmartSoftware.MemoryDb.DependencyInjection;

public class MemoryDbRepositoryRegistrar : RepositoryRegistrarBase<SmartSoftwareMemoryDbContextRegistrationOptions>
{
    public MemoryDbRepositoryRegistrar(SmartSoftwareMemoryDbContextRegistrationOptions options)
        : base(options)
    {
    }

    protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        var memoryDbContext = (MemoryDbContext)Activator.CreateInstance(dbContextType)!;
        return memoryDbContext.GetEntityTypes();
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType)
    {
        return typeof(MemoryDbRepository<,>).MakeGenericType(dbContextType, entityType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
    {
        return typeof(MemoryDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
    }
}
