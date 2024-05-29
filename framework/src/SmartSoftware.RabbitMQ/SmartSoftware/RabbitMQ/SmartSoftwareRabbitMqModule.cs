using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.RabbitMQ;

[DependsOn(
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareThreadingModule)
    )]
public class SmartSoftwareRabbitMqModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<SmartSoftwareRabbitMqOptions>(configuration.GetSection("RabbitMQ"));
        Configure<SmartSoftwareRabbitMqOptions>(options =>
        {
            foreach (var connectionFactory in options.Connections.Values)
            {
                connectionFactory.DispatchConsumersAsync = true;
            }
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        context.ServiceProvider
            .GetRequiredService<IChannelPool>()
            .Dispose();

        context.ServiceProvider
            .GetRequiredService<IConnectionPool>()
            .Dispose();
    }
}
