using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Identity;
using SmartSoftware.Modularity;
using SmartSoftware.Users;

namespace SmartSoftware.PermissionManagement.Identity;

[DependsOn(
    typeof(SmartSoftwareIdentityDomainSharedModule),
    typeof(SmartSoftwarePermissionManagementDomainModule),
    typeof(SmartSoftwareUsersAbstractionModule)
)]
public class SmartSoftwarePermissionManagementDomainIdentityModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<PermissionManagementOptions>(options =>
        {
            options.ManagementProviders.Add<UserPermissionManagementProvider>();
            options.ManagementProviders.Add<RolePermissionManagementProvider>();

            //TODO: Can we prevent duplication of permission names without breaking the design and making the system complicated
            options.ProviderPolicies[UserPermissionValueProvider.ProviderName] = "SmartSoftwareIdentity.Users.ManagePermissions";
            options.ProviderPolicies[RolePermissionValueProvider.ProviderName] = "SmartSoftwareIdentity.Roles.ManagePermissions";
        });
    }
}
