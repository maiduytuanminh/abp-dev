using SmartSoftware.Application;
using SmartSoftware.AutoMapper;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Blogs;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.MediaDescriptors;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitCommonApplicationContractsModule),
    typeof(CmsKitDomainModule),
    typeof(SmartSoftwareDddApplicationModule),
    typeof(SmartSoftwareAutoMapperModule)
)]
public class CmsKitCommonApplicationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitCommonApplicationModule>(validate: true);
        });
    }
}
