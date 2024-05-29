using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Identity.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.Identity;

[DependsOn(typeof(SmartSoftwareIdentityApplicationContractsModule), typeof(SmartSoftwareAspNetCoreMvcModule))]
public class SmartSoftwareIdentityHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareIdentityHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<IdentityResource>()
                .AddBaseTypes(
                    typeof(SmartSoftwareUiResource)
                );
        });
    }
}
