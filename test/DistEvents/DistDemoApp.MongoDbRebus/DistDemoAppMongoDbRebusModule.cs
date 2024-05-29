using Microsoft.Extensions.DependencyInjection;
using Rebus.Persistence.InMem;
using Rebus.Transport.InMem;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.EventBus.Rebus;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.DistributedEvents;

namespace DistDemoApp
{
    [DependsOn(
        typeof(SmartSoftwareMongoDbModule),
        typeof(SmartSoftwareEventBusRebusModule),
        typeof(DistDemoAppSharedModule)
    )]
    public class DistDemoAppMongoDbRebusModule : SmartSoftwareModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<SmartSoftwareRebusEventBusOptions>(options =>
            {
                options.InputQueueName = "eventbus";
                options.Configurer = rebusConfigurer =>
                {
                    rebusConfigurer.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "eventbus"));
                    rebusConfigurer.Subscriptions(s => s.StoreInMemory());
                };
            });
        }

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
