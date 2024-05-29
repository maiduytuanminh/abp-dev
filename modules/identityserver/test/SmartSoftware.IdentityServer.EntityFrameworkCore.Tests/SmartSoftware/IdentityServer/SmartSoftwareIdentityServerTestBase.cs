using SmartSoftware.Testing;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareIdentityServerTestBase : SmartSoftwareIntegratedTest<SmartSoftwareIdentityServerTestEntityFrameworkCoreModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
