using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.AzureServiceBus;
using SmartSoftware.Modularity;

namespace SmartSoftware.EventBus.Azure;

[DependsOn(
    typeof(SmartSoftwareEventBusModule),
    typeof(SmartSoftwareAzureServiceBusModule)
)]
public class SmartSoftwareEventBusAzureModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<SmartSoftwareAzureEventBusOptions>(configuration.GetSection("Azure:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAzureEventBusOptions>>().Value;

        if (!options.IsServiceBusDisabled)
        {
            context
                .ServiceProvider
                .GetRequiredService<AzureDistributedEventBus>()
                .Initialize();
        }

    }
}
