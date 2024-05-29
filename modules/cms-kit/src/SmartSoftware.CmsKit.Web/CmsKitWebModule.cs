using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Admin.Web;
using SmartSoftware.CmsKit.Public.Web;

namespace SmartSoftware.CmsKit.Web;

[DependsOn(
    typeof(CmsKitPublicWebModule),
    typeof(CmsKitAdminWebModule),
    typeof(CmsKitApplicationContractsModule)
    )]
public class CmsKitWebModule : SmartSoftwareModule
{
}
