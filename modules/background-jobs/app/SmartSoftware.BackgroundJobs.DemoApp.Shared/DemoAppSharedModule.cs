using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.BackgroundJobs.DemoApp.Shared.Jobs;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.DemoApp.Shared
{
    [DependsOn(
        typeof(SmartSoftwareBackgroundJobsModule)
        )]
    public class DemoAppSharedModule : SmartSoftwareModule
    {
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            context.ServiceProvider
                .GetRequiredService<SampleJobCreator>()
                .CreateJobs();
        }
    }
}
