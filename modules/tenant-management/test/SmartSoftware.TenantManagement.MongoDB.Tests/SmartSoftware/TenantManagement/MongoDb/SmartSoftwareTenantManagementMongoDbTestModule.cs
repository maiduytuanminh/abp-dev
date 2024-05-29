using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftware.TenantManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwareTenantManagementMongoDbModule),
    typeof(SmartSoftwareTenantManagementTestBaseModule)
    )]
public class SmartSoftwareTenantManagementMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
