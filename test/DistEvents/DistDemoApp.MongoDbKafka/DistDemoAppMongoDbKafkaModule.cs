using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.EventBus.Kafka;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.DistributedEvents;

namespace DistDemoApp
{
    [DependsOn(
        typeof(SmartSoftwareMongoDbModule),
        typeof(SmartSoftwareEventBusKafkaModule),
        typeof(DistDemoAppSharedModule)
    )]
    public class DistDemoAppMongoDbKafkaModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<TodoMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

            Configure<SmartSoftwareDistributedEventBusOptions>(options =>
            {
                options.Outboxes.Configure(config =>
                {
                    config.UseMongoDbContext<TodoMongoDbContext>();
                });

                options.Inboxes.Configure(config =>
                {
                    config.UseMongoDbContext<TodoMongoDbContext>();
                });
            });
        }
    }
}
