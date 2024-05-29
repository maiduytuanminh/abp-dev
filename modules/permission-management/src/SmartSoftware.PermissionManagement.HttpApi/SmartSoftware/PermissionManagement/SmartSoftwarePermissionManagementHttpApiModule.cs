using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.PermissionManagement.HttpApi;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreMvcModule)
    )]
public class SmartSoftwarePermissionManagementHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwarePermissionManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwarePermissionManagementResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }
}
