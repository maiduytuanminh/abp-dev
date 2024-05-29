using SmartSoftware.Autofac;
using SmartSoftware.BackgroundJobs.DemoApp.Shared;
using SmartSoftware.BackgroundJobs.Quartz;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.DemoApp.Quartz;

[DependsOn(
    typeof(DemoAppSharedModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareBackgroundJobsQuartzModule)
)]
public class DemoAppQuartzModule : SmartSoftwareModule
{

}
