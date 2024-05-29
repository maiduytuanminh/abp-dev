using MongoDB.Driver;

namespace SmartSoftware.MongoDB.TestApp.FifthContext;

public class FifthDbContext : SmartSoftwareMongoDbContext, IFifthDbContext
{
    public IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }

    public IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }
}
