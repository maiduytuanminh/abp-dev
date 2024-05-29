using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.TenantManagement;

public abstract class TenantManagementTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
