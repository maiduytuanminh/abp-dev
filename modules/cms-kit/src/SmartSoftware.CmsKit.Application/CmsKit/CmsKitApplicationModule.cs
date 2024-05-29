using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Admin;
using SmartSoftware.CmsKit.Public;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitPublicApplicationModule),
    typeof(CmsKitAdminApplicationModule),
    typeof(CmsKitApplicationContractsModule)
    )]
public class CmsKitApplicationModule : SmartSoftwareModule
{

}
