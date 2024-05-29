using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.AuditLogging;

public class AuditLoggingTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
