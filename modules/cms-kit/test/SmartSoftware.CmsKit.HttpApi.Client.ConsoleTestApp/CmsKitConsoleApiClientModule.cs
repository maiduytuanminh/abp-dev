using SmartSoftware.Http.Client.IdentityModel;
using SmartSoftware.Modularity;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitHttpApiClientModule),
    typeof(SmartSoftwareHttpClientIdentityModelModule)
    )]
public class CmsKitConsoleApiClientModule : SmartSoftwareModule
{

}
