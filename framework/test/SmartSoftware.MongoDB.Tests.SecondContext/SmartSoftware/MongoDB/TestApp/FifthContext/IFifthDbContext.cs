using MongoDB.Driver;

namespace SmartSoftware.MongoDB.TestApp.FifthContext;

public interface IFifthDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; }

    IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; }
}
