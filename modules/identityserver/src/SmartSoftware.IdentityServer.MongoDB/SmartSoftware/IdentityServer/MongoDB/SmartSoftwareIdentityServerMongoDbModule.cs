using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.IdentityServer.MongoDB;

[DependsOn(
    typeof(SmartSoftwareIdentityServerDomainModule),
    typeof(SmartSoftwareMongoDbModule)
)]
public class SmartSoftwareIdentityServerMongoDbModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<IIdentityServerBuilder>(
            builder =>
            {
                builder.AddSmartSoftwareStores();
            }
        );
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<SmartSoftwareIdentityServerMongoDbContext>(options =>
        {
            options.AddRepository<ApiResource, MongoApiResourceRepository>();
            options.AddRepository<ApiScope, MongoApiScopeRepository>();
            options.AddRepository<IdentityResource, MongoIdentityResourceRepository>();
            options.AddRepository<Client, MongoClientRepository>();
            options.AddRepository<PersistedGrant, MongoPersistentGrantRepository>();
            options.AddRepository<DeviceFlowCodes, MongoDeviceFlowCodesRepository>();
        });
    }
}
