using System;
using SmartSoftware.Data;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.MongoDB;
using SmartSoftware.Uow;

namespace SmartSoftware.Identity.MongoDB;

[DependsOn(
    typeof(SmartSoftwareIdentityTestBaseModule),
    typeof(SmartSoftwarePermissionManagementMongoDbModule),
    typeof(SmartSoftwareIdentityMongoDbModule)
)]
public class SmartSoftwareIdentityMongoDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });

        Configure<SmartSoftwareUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
