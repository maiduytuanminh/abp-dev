using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.DistributedEvents;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.EventBus.RabbitMq;
using SmartSoftware.Modularity;

namespace DistDemoApp
{
    [DependsOn(
        typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
        typeof(SmartSoftwareEventBusRabbitMqModule),
        typeof(DistDemoAppSharedModule)
    )]
    public class DistDemoAppEfCoreRabbitMqModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSmartSoftwareDbContext<TodoDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

            Configure<SmartSoftwareDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
            
            Configure<SmartSoftwareDistributedEventBusOptions>(options =>
            {
                options.Outboxes.Configure(config =>
                {
                    config.UseDbContext<TodoDbContext>();
                });
                
                options.Inboxes.Configure(config =>
                {
                    config.UseDbContext<TodoDbContext>();
                });
            });
        }
    }
}