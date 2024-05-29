using SmartSoftware.Testing;

namespace SmartSoftware.MemoryDb;

public abstract class MemoryDbTestBase : SmartSoftwareIntegratedTest<SmartSoftwareMemoryDbTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
