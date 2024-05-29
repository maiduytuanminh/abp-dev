using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.SettingManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwareSettingManagementMongoDbModule),
    typeof(SmartSoftwareSettingManagementTestBaseModule)
    )]
public class SmartSoftwareSettingManagementMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
