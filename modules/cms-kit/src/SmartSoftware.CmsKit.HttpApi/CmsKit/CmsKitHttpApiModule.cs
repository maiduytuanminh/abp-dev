using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Admin;
using SmartSoftware.CmsKit.Public;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitAdminHttpApiModule),
    typeof(CmsKitPublicHttpApiModule),
    typeof(CmsKitApplicationContractsModule)
    )]
public class CmsKitHttpApiModule : SmartSoftwareModule
{

}
