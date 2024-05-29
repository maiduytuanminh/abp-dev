using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.CmsKit.Public;

[DependsOn(
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(CmsKitCommonHttpApiModule))]
public class CmsKitPublicHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitPublicHttpApiModule).Assembly);
        });
    }
}
