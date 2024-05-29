using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.AzureServiceBus;

[DependsOn(
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareThreadingModule)
)]
public class SmartSoftwareAzureServiceBusModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<SmartSoftwareAzureServiceBusOptions>(configuration.GetSection("Azure:ServiceBus"));
    }
}
