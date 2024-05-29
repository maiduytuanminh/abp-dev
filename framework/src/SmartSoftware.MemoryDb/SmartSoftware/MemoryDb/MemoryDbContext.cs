using System;
using System.Collections.Generic;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.MemoryDb;

public abstract class MemoryDbContext : ISingletonDependency
{
    private static readonly Type[] EmptyTypeList = new Type[0];

    public virtual IReadOnlyList<Type> GetEntityTypes()
    {
        return EmptyTypeList;
    }
}
