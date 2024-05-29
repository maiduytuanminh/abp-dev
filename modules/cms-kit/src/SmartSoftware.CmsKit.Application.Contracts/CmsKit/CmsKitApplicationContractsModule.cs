using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Admin;
using SmartSoftware.CmsKit.Public;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitPublicApplicationContractsModule),
    typeof(CmsKitAdminApplicationContractsModule)
    )]
public class CmsKitApplicationContractsModule : SmartSoftwareModule
{

}
