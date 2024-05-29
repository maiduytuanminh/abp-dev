using SmartSoftware.Testing;

namespace SmartSoftware.EventBus.Distributed;

public abstract class LocalDistributedEventBusTestBase : SmartSoftwareIntegratedTest<EventBusTestModule>
{
    protected IDistributedEventBus DistributedEventBus;

    protected LocalDistributedEventBusTestBase()
    {
        DistributedEventBus = GetRequiredService<LocalDistributedEventBus>();
    }

    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
