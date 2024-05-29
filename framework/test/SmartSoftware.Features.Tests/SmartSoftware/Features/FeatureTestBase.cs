using SmartSoftware.Testing;

namespace SmartSoftware.Features;

public class FeatureTestBase : SmartSoftwareIntegratedTest<SmartSoftwareFeaturesTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
