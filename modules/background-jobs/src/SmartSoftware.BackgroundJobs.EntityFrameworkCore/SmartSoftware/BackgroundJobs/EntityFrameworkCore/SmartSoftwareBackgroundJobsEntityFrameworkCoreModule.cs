using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsDomainModule),
    typeof(SmartSoftwareEntityFrameworkCoreModule)
)]
public class SmartSoftwareBackgroundJobsEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<BackgroundJobsDbContext>(options =>
        {
            options.AddRepository<BackgroundJobRecord, EfCoreBackgroundJobRepository>();
        });
    }
}
