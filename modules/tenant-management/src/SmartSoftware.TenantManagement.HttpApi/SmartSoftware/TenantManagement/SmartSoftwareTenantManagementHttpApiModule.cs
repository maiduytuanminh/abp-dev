using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.FeatureManagement;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.TenantManagement.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.TenantManagement;

[DependsOn(
    typeof(SmartSoftwareTenantManagementApplicationContractsModule),
    typeof(SmartSoftwareFeatureManagementHttpApiModule),
    typeof(SmartSoftwareAspNetCoreMvcModule)
    )]
public class SmartSoftwareTenantManagementHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareTenantManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartSoftwareTenantManagementResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareFeatureManagementResource),
                    typeof(SmartSoftwareUiResource));
        });
    }
}
