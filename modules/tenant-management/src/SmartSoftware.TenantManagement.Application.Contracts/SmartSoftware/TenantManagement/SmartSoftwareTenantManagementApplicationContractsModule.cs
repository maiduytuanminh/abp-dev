using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.TenantManagement;

[DependsOn(
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareTenantManagementDomainSharedModule),
    typeof(SmartSoftwareAuthorizationAbstractionsModule)
    )]
public class SmartSoftwareTenantManagementApplicationContractsModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToApi(
                    TenantManagementModuleExtensionConsts.ModuleName,
                    TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                    getApiTypes: new[] { typeof(TenantDto) },
                    createApiTypes: new[] { typeof(TenantCreateDto) },
                    updateApiTypes: new[] { typeof(TenantUpdateDto) }
                );
        });
    }
}
