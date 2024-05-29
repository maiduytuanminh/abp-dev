using SmartSoftware.Account;
using SmartSoftware.AutoMapper;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement;
using SmartSoftware.SettingManagement;
using SmartSoftware.TenantManagement;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameDomainModule),
    typeof(SmartSoftwareAccountApplicationModule),
    typeof(MyProjectNameApplicationContractsModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareSettingManagementApplicationModule)
    )]
public class MyProjectNameApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<MyProjectNameApplicationModule>();
        });
    }
}
