using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.RabbitMQ;

namespace SmartSoftware.EventBus.RabbitMq;

[DependsOn(
    typeof(SmartSoftwareEventBusModule),
    typeof(SmartSoftwareRabbitMqModule))]
public class SmartSoftwareEventBusRabbitMqModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<SmartSoftwareRabbitMqEventBusOptions>(configuration.GetSection("RabbitMQ:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<RabbitMqDistributedEventBus>()
            .Initialize();
    }
}
