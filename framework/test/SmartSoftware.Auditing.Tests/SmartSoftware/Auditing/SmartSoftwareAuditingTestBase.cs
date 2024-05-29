using SmartSoftware.Testing;

namespace SmartSoftware.Auditing;

public class SmartSoftwareAuditingTestBase : SmartSoftwareIntegratedTest<SmartSoftwareAuditingTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
