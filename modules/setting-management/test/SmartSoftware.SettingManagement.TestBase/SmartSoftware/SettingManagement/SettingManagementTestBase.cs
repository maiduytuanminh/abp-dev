using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.SettingManagement;

public class SettingManagementTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
