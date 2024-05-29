using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.BackgroundJobs.MongoDB;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsTestBaseModule),
    typeof(SmartSoftwareBackgroundJobsMongoDbModule)
    )]
public class SmartSoftwareBackgroundJobsMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
