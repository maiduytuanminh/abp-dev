using System;
using System.Collections.Generic;
using SmartSoftware.MemoryDb;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;

namespace SmartSoftware.TestApp.MemoryDb;

public class TestAppMemoryDbContext : MemoryDbContext
{
    private static readonly Type[] EntityTypeList = {
            typeof(Person),
            typeof(EntityWithIntPk),
            typeof(Product),
            typeof(AppEntityWithNavigations)
    };

    public override IReadOnlyList<Type> GetEntityTypes()
    {
        return EntityTypeList;
    }
}
