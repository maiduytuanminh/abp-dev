using SmartSoftware.Testing;

namespace SmartSoftware.AspNetCore.SignalR;

public abstract class SmartSoftwareAspNetCoreSignalRTestBase : SmartSoftwareIntegratedTest<SmartSoftwareAspNetCoreSignalRTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
