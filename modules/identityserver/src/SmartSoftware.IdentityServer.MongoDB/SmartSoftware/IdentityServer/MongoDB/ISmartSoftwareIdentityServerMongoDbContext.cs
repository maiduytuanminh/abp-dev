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
public interface ISmartSoftwareIdentityServerMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<ApiResource> ApiResources { get; }

    IMongoCollection<ApiScope> ApiScopes { get; }

    IMongoCollection<Client> Clients { get; }

    IMongoCollection<IdentityResource> IdentityResources { get; }

    IMongoCollection<PersistedGrant> PersistedGrants { get; }

    IMongoCollection<DeviceFlowCodes> DeviceFlowCodes { get; }
}
