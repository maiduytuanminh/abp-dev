using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsApplicationContractsModule),
        typeof(SmartSoftwareAspNetCoreMvcModule)
        )]
    public class DocsHttpApiModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsHttpApiModule).Assembly);
            });
        }
    }
}
