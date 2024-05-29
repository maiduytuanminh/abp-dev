using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.TextTemplating;

public abstract class SmartSoftwareTextTemplatingTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
