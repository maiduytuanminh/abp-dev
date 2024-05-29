using SmartSoftware.Testing;

namespace SmartSoftware.IdentityModel;

public abstract class SmartSoftwareIdentityModelTestBase : SmartSoftwareIntegratedTest<SmartSoftwareIdentityModelTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
