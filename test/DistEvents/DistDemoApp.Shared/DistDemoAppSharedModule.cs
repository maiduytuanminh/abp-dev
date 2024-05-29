using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using SmartSoftware.Autofac;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus;
using SmartSoftware.Modularity;

namespace DistDemoApp
{
    [DependsOn(
        typeof(SmartSoftwareAutofacModule),
        typeof(SmartSoftwareDddDomainModule),
        typeof(SmartSoftwareEventBusModule)
        )]
    public class DistDemoAppSharedModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddHostedService<DistDemoAppHostedService>();

            Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<TodoItem, TodoItemEto>();
                options.AutoEventSelectors.Add<TodoItem>();
            });

            context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            });
        }
    }
}
