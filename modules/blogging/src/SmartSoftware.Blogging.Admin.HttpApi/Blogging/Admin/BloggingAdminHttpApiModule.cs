using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.Blogging.Admin
{
    [DependsOn(
        typeof(BloggingAdminApplicationContractsModule),
        typeof(SmartSoftwareAspNetCoreMvcModule))]
    public class BloggingAdminHttpApiModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BloggingAdminHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<BloggingResource>()
                    .AddBaseTypes(typeof(SmartSoftwareUiResource));
            });
        }
    }
}
