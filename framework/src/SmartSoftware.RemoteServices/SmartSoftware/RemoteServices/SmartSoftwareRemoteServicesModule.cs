using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.RemoteServices;

[DependsOn(typeof(SmartSoftwareMultiTenancyAbstractionsModule))]
public class SmartSoftwareRemoteServicesModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<SmartSoftwareRemoteServiceOptions>(configuration);
    }
}