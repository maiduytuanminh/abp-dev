using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.BackgroundJobs.MongoDB;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwareBackgroundJobsMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<BackgroundJobsMongoDbContext>(options =>
        {
            options.AddRepository<BackgroundJobRecord, MongoBackgroundJobRepository>();
        });
    }
}
