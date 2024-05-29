using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TenantManagement.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareTenantManagementDbProperties.ConnectionStringName)]
public class TenantManagementMongoDbContext : SmartSoftwareMongoDbContext, ITenantManagementMongoDbContext
{
    public IMongoCollection<Tenant> Tenants => Collection<Tenant>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureTenantManagement();
    }
}
