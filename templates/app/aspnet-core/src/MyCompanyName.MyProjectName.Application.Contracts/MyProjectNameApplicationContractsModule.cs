using SmartSoftware.Account;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.PermissionManagement;
using SmartSoftware.SettingManagement;
using SmartSoftware.TenantManagement;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameDomainSharedModule),
    typeof(SmartSoftwareAccountApplicationContractsModule),
    typeof(SmartSoftwareFeatureManagementApplicationContractsModule),
    typeof(SmartSoftwareIdentityApplicationContractsModule),
    typeof(SmartSoftwarePermissionManagementApplicationContractsModule),
    typeof(SmartSoftwareSettingManagementApplicationContractsModule),
    typeof(SmartSoftwareTenantManagementApplicationContractsModule),
    typeof(SmartSoftwareObjectExtendingModule)
)]
public class MyProjectNameApplicationContractsModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MyProjectNameDtoExtensions.Configure();
    }
}
