using SmartSoftware.Testing;

namespace SmartSoftware.DistributedLocking;

public class SmartSoftwareDistributedLockingAbstractionsTestBase : SmartSoftwareIntegratedTest<SmartSoftwareDistributedLockingAbstractionsTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
