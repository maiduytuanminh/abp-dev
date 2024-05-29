using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Blogging.Localization;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Blogging.Files;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingApplicationContractsModule),
        typeof(SmartSoftwareAspNetCoreMvcModule))]
    public class BloggingHttpApiModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BloggingHttpApiModule).Assembly);
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

            Configure<SmartSoftwareAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(FileUploadInputDto));
            });
        }
    }
}
