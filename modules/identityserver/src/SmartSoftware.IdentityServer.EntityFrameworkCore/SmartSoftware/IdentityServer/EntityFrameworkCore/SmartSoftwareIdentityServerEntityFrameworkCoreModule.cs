using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.Modularity;

namespace SmartSoftware.IdentityServer.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareIdentityServerDomainModule),
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareIdentityServerEntityFrameworkCoreModule : SmartSoftwareModule
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
        context.Services.AddSmartSoftwareDbContext<IdentityServerDbContext>(options =>
        {
            options.AddDefaultRepositories<IIdentityServerDbContext>();

            options.AddRepository<Client, ClientRepository>();
            options.AddRepository<ApiResource, ApiResourceRepository>();
            options.AddRepository<ApiScope, ApiScopeRepository>();
            options.AddRepository<IdentityResource, IdentityResourceRepository>();
            options.AddRepository<PersistedGrant, PersistentGrantRepository>();
            options.AddRepository<DeviceFlowCodes, DeviceFlowCodesRepository>();
        });
    }
}
