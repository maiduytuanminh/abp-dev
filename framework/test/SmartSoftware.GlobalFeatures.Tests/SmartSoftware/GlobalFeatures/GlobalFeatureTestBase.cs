using SmartSoftware.Testing;

namespace SmartSoftware.GlobalFeatures;

public abstract class GlobalFeatureTestBase : SmartSoftwareIntegratedTest<GlobalFeatureTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
