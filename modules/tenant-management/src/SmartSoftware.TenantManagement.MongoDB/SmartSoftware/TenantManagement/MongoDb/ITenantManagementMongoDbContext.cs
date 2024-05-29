using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.TenantManagement.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareTenantManagementDbProperties.ConnectionStringName)]
public interface ITenantManagementMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<Tenant> Tenants { get; }
}
