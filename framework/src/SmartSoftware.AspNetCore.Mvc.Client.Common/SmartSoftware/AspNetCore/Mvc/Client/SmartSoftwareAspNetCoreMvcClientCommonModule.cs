using Microsoft.Extensions.DependencyInjection;
using Pages.SmartSoftware.MultiTenancy.ClientProxies;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using SmartSoftware.Authorization;
using SmartSoftware.Caching;
using SmartSoftware.Features;
using SmartSoftware.Http.Client;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Security.Claims;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AspNetCore.Mvc.Client;

[DependsOn(
    typeof(SmartSoftwareHttpClientModule),
    typeof(SmartSoftwareAspNetCoreMvcContractsModule),
    typeof(SmartSoftwareCachingModule),
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareFeaturesModule),
    typeof(SmartSoftwareVirtualFileSystemModule)
)]
public class SmartSoftwareAspNetCoreMvcClientCommonModule : SmartSoftwareModule
{
    public const string RemoteServiceName = "SmartSoftwareMvcClient";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(typeof(SmartSoftwareAspNetCoreMvcContractsModule).Assembly, RemoteServiceName);

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAspNetCoreMvcClientCommonModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.GlobalContributors.Add<RemoteLocalizationContributor>();
        });

        context.Services.AddTransient<SmartSoftwareApplicationConfigurationClientProxy>();
        context.Services.AddTransient<SmartSoftwareTenantClientProxy>();

        var ssClaimsPrincipalFactoryOptions = context.Services.ExecutePreConfiguredActions<SmartSoftwareClaimsPrincipalFactoryOptions>();
        if (ssClaimsPrincipalFactoryOptions.IsRemoteRefreshEnabled)
        {
            context.Services.AddTransient<RemoteDynamicClaimsPrincipalContributor>();
            context.Services.AddTransient<RemoteDynamicClaimsPrincipalContributorCache>();
        }
    }
}
