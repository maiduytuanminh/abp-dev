using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Caching;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;

namespace SmartSoftware.IdentityModel;

[DependsOn(
    typeof(SmartSoftwareThreadingModule),
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareCachingModule)
    )]
public class SmartSoftwareIdentityModelModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddHttpClient(IdentityModelAuthenticationService.HttpClientName);

        Configure<SmartSoftwareIdentityClientOptions>(configuration);
    }
}
