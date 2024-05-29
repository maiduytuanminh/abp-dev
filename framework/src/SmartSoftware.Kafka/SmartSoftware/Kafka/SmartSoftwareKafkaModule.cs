using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.Kafka;

[DependsOn(
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareThreadingModule)
)]
public class SmartSoftwareKafkaModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<SmartSoftwareKafkaOptions>(configuration.GetSection("Kafka"));
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        context.ServiceProvider
            .GetRequiredService<IConsumerPool>()
            .Dispose();

        context.ServiceProvider
            .GetRequiredService<IProducerPool>()
            .Dispose();
    }
}
