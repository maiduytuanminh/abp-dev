using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AuditLogging.MongoDB;
using SmartSoftware.BackgroundJobs.MongoDB;
using SmartSoftware.FeatureManagement.MongoDB;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict.MongoDB;
using SmartSoftware.PermissionManagement.MongoDB;
using SmartSoftware.SettingManagement.MongoDB;
using SmartSoftware.TenantManagement.MongoDB;
using SmartSoftware.Uow;

namespace MyCompanyName.MyProjectName.MongoDB;

[DependsOn(
    typeof(MyProjectNameDomainModule),
    typeof(SmartSoftwarePermissionManagementMongoDbModule),
    typeof(SmartSoftwareSettingManagementMongoDbModule),
    typeof(SmartSoftwareIdentityMongoDbModule),
    typeof(SmartSoftwareOpenIddictMongoDbModule),
    typeof(SmartSoftwareBackgroundJobsMongoDbModule),
    typeof(SmartSoftwareAuditLoggingMongoDbModule),
    typeof(SmartSoftwareTenantManagementMongoDbModule),
    typeof(SmartSoftwareFeatureManagementMongoDbModule)
    )]
public class MyProjectNameMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<MyProjectNameMongoDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<SmartSoftwareUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
