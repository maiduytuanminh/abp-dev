using MongoDB.Driver;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.TestApp.FifthContext;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TestApp.MongoDb;

[ReplaceDbContext(typeof(IFifthDbContext), MultiTenancySides.Tenant)]
public class TenantTestAppDbContext : SmartSoftwareMongoDbContext, IFifthDbContext
{
    public IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity => Collection<FifthDbContextDummyEntity>();

    public IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity => Collection<FifthDbContextMultiTenantDummyEntity>();
}
