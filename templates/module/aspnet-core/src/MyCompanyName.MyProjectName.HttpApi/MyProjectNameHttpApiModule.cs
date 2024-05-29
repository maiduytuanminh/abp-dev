using Localization.Resources.SmartSoftwareUi;
using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreMvcModule))]
public class MyProjectNameHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MyProjectNameHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MyProjectNameResource>()
                .AddBaseTypes(typeof(SmartSoftwareUiResource));
        });
    }
}
