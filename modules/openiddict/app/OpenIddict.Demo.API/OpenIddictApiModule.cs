using SmartSoftware.AspNetCore.Authentication.JwtBearer;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Modularity;

namespace OpenIddict.Demo.API;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAspNetCoreAuthenticationJwtBearerModule)
)]
public class OpenIddictApiModule : SmartSoftwareModule
{

}
