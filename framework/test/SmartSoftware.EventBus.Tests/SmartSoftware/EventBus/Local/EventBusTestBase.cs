using SmartSoftware.Testing;

namespace SmartSoftware.EventBus.Local;

public abstract class EventBusTestBase : SmartSoftwareIntegratedTest<EventBusTestModule>
{
    protected ILocalEventBus LocalEventBus;

    protected EventBusTestBase()
    {
        LocalEventBus = GetRequiredService<ILocalEventBus>();
    }

    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
