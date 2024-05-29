using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Dapr;
using SmartSoftware.Modularity;

namespace SmartSoftware.EventBus.Dapr;

[DependsOn(
    typeof(SmartSoftwareEventBusModule),
    typeof(SmartSoftwareDaprModule)
    )]
public class SmartSoftwareEventBusDaprModule : SmartSoftwareModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<DaprDistributedEventBus>()
            .Initialize();
    }
}
