using SmartSoftware.Testing;

namespace SmartSoftware.TestApp;

public class TestAppTestBase : SmartSoftwareIntegratedTest<TestAppTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
