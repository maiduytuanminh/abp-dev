using SmartSoftware.Testing;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareIdentityServerDomainTestBase : SmartSoftwareIntegratedTest<SmartSoftwareIdentityServerDomainTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
