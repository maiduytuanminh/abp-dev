using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;

namespace SmartSoftware.FeatureManagement.MongoDB;

[DependsOn(
    typeof(FeatureManagementTestBaseModule),
    typeof(SmartSoftwareFeatureManagementMongoDbModule)
    )]
public class SmartSoftwareFeatureManagementMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });

    }
}
