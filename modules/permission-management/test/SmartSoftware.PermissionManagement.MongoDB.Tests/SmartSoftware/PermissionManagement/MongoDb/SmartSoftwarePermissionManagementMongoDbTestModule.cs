using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.PermissionManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementMongoDbModule),
    typeof(SmartSoftwarePermissionManagementTestBaseModule))]
public class SmartSoftwarePermissionManagementMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
