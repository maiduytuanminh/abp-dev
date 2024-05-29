using SmartSoftware.Authorization.Permissions;
using SmartSoftware.IdentityServer;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement.IdentityServer;

[DependsOn(
    typeof(SmartSoftwareIdentityServerDomainSharedModule),
    typeof(SmartSoftwarePermissionManagementDomainModule)
)]
public class SmartSoftwarePermissionManagementDomainIdentityServerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<PermissionManagementOptions>(options =>
        {
            options.ManagementProviders.Add<ClientPermissionManagementProvider>();

            options.ProviderPolicies[ClientPermissionValueProvider.ProviderName] = "IdentityServer.Client.ManagePermissions";
        });
    }
}
