using SmartSoftware.BackgroundJobs.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsEntityFrameworkCoreTestModule)
    )]
public class SmartSoftwareBackgroundJobsDomainTestModule : SmartSoftwareModule
{

}
