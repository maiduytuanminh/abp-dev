using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Authentication.JwtBearer.DynamicClaims;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;
using SmartSoftware.Security;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Authentication.JwtBearer;

[DependsOn(typeof(SmartSoftwareSecurityModule), typeof(SmartSoftwareCachingModule))]
public class SmartSoftwareAspNetCoreAuthenticationJwtBearerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
        context.Services.AddHttpContextAccessor();

        if (context.Services.ExecutePreConfiguredActions<WebRemoteDynamicClaimsPrincipalContributorOptions>().IsEnabled &&
            context.Services.ExecutePreConfiguredActions<SmartSoftwareClaimsPrincipalFactoryOptions>().IsRemoteRefreshEnabled)
        {
            context.Services.AddTransient<WebRemoteDynamicClaimsPrincipalContributor>();
            context.Services.AddTransient<WebRemoteDynamicClaimsPrincipalContributorCache>();
        }
    }
}
