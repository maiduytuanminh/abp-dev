using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Admin;
using SmartSoftware.CmsKit.Public;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitAdminHttpApiClientModule),
    typeof(CmsKitPublicHttpApiClientModule),
    typeof(CmsKitApplicationContractsModule)
    )]
public class CmsKitHttpApiClientModule : SmartSoftwareModule
{

}
