using SmartSoftware.Testing;

namespace SmartSoftware.BackgroundJobs;

public abstract class BackgroundJobsTestBase : SmartSoftwareIntegratedTest<SmartSoftwareBackgroundJobsTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
