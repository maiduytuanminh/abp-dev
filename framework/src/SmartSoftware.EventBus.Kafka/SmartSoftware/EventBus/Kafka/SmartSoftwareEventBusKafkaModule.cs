using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Kafka;
using SmartSoftware.Modularity;

namespace SmartSoftware.EventBus.Kafka;

[DependsOn(
    typeof(SmartSoftwareEventBusModule),
    typeof(SmartSoftwareKafkaModule))]
public class SmartSoftwareEventBusKafkaModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<SmartSoftwareKafkaEventBusOptions>(configuration.GetSection("Kafka:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<KafkaDistributedEventBus>()
            .Initialize();
    }
}
