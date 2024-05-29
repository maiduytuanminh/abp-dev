using SmartSoftware;
using SmartSoftware.Modularity;
using SmartSoftware.Testing;

namespace SmartSoftware.BackgroundJobs;

public abstract class BackgroundJobsTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
