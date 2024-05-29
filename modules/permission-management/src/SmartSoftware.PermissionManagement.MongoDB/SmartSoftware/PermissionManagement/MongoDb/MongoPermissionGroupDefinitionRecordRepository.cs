using System;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;

namespace SmartSoftware.PermissionManagement.MongoDB;

public class MongoPermissionGroupDefinitionRecordRepository :
    MongoDbRepository<IPermissionManagementMongoDbContext, PermissionGroupDefinitionRecord, Guid>,
    IPermissionGroupDefinitionRecordRepository
{
    public MongoPermissionGroupDefinitionRecordRepository(
        IMongoDbContextProvider<IPermissionManagementMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}