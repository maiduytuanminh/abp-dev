using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.Identity;

public abstract class SmartSoftwareIdentityTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
