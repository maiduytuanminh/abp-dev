using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict;

namespace SmartSoftware.PermissionManagement.OpenIddict;

[DependsOn(
    typeof(SmartSoftwareOpenIddictDomainSharedModule),
    typeof(SmartSoftwarePermissionManagementDomainModule)
)]
public class SmartSoftwarePermissionManagementDomainOpenIddictModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<PermissionManagementOptions>(options =>
        {
            options.ManagementProviders.Add<ApplicationPermissionManagementProvider>();
            options.ProviderPolicies[ClientPermissionValueProvider.ProviderName] = "OpenIddictPro.Application.ManagePermissions";
        });
    }
}
