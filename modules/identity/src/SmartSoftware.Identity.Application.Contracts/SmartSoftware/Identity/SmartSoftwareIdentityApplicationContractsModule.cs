using SmartSoftware.Authorization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.PermissionManagement;
using SmartSoftware.Users;
using SmartSoftware.Threading;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareIdentityDomainSharedModule),
    typeof(SmartSoftwareUsersAbstractionModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwarePermissionManagementApplicationContractsModule)
    )]
public class SmartSoftwareIdentityApplicationContractsModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.Role,
                getApiTypes: new[] { typeof(IdentityRoleDto) },
                createApiTypes: new[] { typeof(IdentityRoleCreateDto) },
                updateApiTypes: new[] { typeof(IdentityRoleUpdateDto) }
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.User,
                getApiTypes: new[] { typeof(IdentityUserDto) },
                createApiTypes: new[] { typeof(IdentityUserCreateDto) },
                updateApiTypes: new[] { typeof(IdentityUserUpdateDto) }
            );
        });
    }
}
