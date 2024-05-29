using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(CmsKitCommonApplicationContractsModule)
    )]
public class CmsKitCommonHttpApiModule : SmartSoftwareModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitCommonHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CmsKitResource>()
                .AddBaseTypes(typeof(SmartSoftwareUiResource));
        });
    }
}
