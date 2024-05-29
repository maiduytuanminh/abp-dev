using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Authentication.OAuth;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.RemoteServices;

namespace SmartSoftware.AspNetCore.Authentication.OpenIdConnect;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareAspNetCoreAuthenticationOAuthModule),
    typeof(SmartSoftwareRemoteServicesModule)
    )]
public class SmartSoftwareAspNetCoreAuthenticationOpenIdConnectModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
    }
}
