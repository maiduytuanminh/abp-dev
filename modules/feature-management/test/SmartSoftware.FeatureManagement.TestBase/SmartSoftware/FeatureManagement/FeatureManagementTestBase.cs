using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.FeatureManagement;

public abstract class FeatureManagementTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
