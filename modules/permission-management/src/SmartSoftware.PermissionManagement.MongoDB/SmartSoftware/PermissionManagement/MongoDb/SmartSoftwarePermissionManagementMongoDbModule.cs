using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.PermissionManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwarePermissionManagementMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<PermissionManagementMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<IPermissionManagementMongoDbContext>();

            options.AddRepository<PermissionGroupDefinitionRecord, MongoPermissionGroupDefinitionRecordRepository>();
            options.AddRepository<PermissionDefinitionRecord, MongoPermissionDefinitionRecordRepository>();
            options.AddRepository<PermissionGrant, MongoPermissionGrantRepository>();
        });
    }
}
