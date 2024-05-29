using Localization.Resources.SmartSoftwareUi;
using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.Account;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.SettingManagement;
using SmartSoftware.TenantManagement;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameApplicationContractsModule),
    typeof(SmartSoftwareAccountHttpApiModule),
    typeof(SmartSoftwareIdentityHttpApiModule),
    typeof(SmartSoftwarePermissionManagementHttpApiModule),
    typeof(SmartSoftwareTenantManagementHttpApiModule),
    typeof(SmartSoftwareFeatureManagementHttpApiModule),
    typeof(SmartSoftwareSettingManagementHttpApiModule)
    )]
public class MyProjectNameHttpApiModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MyProjectNameResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }
}
