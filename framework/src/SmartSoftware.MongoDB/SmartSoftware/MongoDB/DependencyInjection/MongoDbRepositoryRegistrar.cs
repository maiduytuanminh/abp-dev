using System;
using System.Collections.Generic;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.MongoDB;

namespace SmartSoftware.MongoDB.DependencyInjection;

public class MongoDbRepositoryRegistrar : RepositoryRegistrarBase<SmartSoftwareMongoDbContextRegistrationOptions>
{
    public MongoDbRepositoryRegistrar(SmartSoftwareMongoDbContextRegistrationOptions options)
        : base(options)
    {

    }

    protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return MongoDbContextHelper.GetEntityTypes(dbContextType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType)
    {
        return typeof(MongoDbRepository<,>).MakeGenericType(dbContextType, entityType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
    {
        return typeof(MongoDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
    }
}
