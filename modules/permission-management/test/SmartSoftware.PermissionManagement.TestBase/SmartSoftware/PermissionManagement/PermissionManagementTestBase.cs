using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.PermissionManagement;

public abstract class PermissionManagementTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
