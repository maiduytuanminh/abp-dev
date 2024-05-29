using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Localization;

namespace SmartSoftware.SettingManagement;

[DependsOn(
    typeof(SmartSoftwareSettingManagementApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreMvcModule))]
public class SmartSoftwareSettingManagementHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareSettingManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwareSettingManagementResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }
}
