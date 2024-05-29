using MongoDB.Driver;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.TestApp.FifthContext;

namespace SmartSoftware.TestApp.MongoDb;

public class HostTestAppDbContext : SmartSoftwareMongoDbContext, IFifthDbContext
{
    public IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity => Collection<FifthDbContextDummyEntity>();

    public IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity => Collection<FifthDbContextMultiTenantDummyEntity>();
}
