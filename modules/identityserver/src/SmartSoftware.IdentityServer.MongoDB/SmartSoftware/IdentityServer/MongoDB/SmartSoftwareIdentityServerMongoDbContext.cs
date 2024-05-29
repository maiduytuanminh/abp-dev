using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.IdentityServer.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareIdentityServerDbProperties.ConnectionStringName)]
public class SmartSoftwareIdentityServerMongoDbContext : SmartSoftwareMongoDbContext, ISmartSoftwareIdentityServerMongoDbContext
{
    public IMongoCollection<ApiResource> ApiResources => Collection<ApiResource>();

    public IMongoCollection<ApiScope> ApiScopes => Collection<ApiScope>();

    public IMongoCollection<Client> Clients => Collection<Client>();

    public IMongoCollection<IdentityResource> IdentityResources => Collection<IdentityResource>();

    public IMongoCollection<PersistedGrant> PersistedGrants => Collection<PersistedGrant>();

    public IMongoCollection<DeviceFlowCodes> DeviceFlowCodes => Collection<DeviceFlowCodes>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureIdentityServer();
    }
}
