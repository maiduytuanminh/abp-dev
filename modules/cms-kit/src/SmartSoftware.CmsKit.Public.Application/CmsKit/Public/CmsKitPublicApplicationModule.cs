using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AutoMapper;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;

namespace SmartSoftware.CmsKit.Public;

[DependsOn(
    typeof(CmsKitCommonApplicationModule),
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(SmartSoftwareCachingModule)
    )]
public class CmsKitPublicApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsKitPublicApplicationModule>();

        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitPublicApplicationModule>(validate: true);
        });
    }
}
