using SmartSoftware.AspNetCore.Authentication.OpenIdConnect;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Modularity;

namespace OpenIddict.Demo.Client.Mvc;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAspNetCoreAuthenticationOpenIdConnectModule)
)]
public class OpenIddictMvcModule : SmartSoftwareModule
{

}
