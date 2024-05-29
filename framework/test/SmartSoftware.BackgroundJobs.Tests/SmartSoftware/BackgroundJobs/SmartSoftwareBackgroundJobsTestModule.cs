using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareBackgroundJobsTestModule : SmartSoftwareModule
{

}
