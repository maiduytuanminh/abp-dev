using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Admin.MediaDescriptors;

namespace SmartSoftware.CmsKit.Admin;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(CmsKitCommonHttpApiModule)
    )]
public class CmsKitAdminHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitAdminHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CreateMediaInputWithStream));
        });
    }
}
