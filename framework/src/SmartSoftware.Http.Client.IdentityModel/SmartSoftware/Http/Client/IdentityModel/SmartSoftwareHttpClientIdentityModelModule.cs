using SmartSoftware.IdentityModel;
using SmartSoftware.Modularity;

namespace SmartSoftware.Http.Client.IdentityModel;

[DependsOn(
    typeof(SmartSoftwareHttpClientModule),
    typeof(SmartSoftwareIdentityModelModule)
    )]
public class SmartSoftwareHttpClientIdentityModelModule : SmartSoftwareModule
{

}
