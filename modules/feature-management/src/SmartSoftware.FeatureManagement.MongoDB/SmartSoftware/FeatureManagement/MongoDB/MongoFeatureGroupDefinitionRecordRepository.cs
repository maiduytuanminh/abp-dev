using System;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;

namespace SmartSoftware.FeatureManagement.MongoDB;

public class MongoFeatureGroupDefinitionRecordRepository :
    MongoDbRepository<IFeatureManagementMongoDbContext, FeatureGroupDefinitionRecord, Guid>,
    IFeatureGroupDefinitionRecordRepository
{
    public MongoFeatureGroupDefinitionRecordRepository(
        IMongoDbContextProvider<IFeatureManagementMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
