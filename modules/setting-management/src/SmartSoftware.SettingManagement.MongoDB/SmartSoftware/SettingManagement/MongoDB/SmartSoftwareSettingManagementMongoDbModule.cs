using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.SettingManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwareSettingManagementDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwareSettingManagementMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<SettingManagementMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<ISettingManagementMongoDbContext>();

            options.AddRepository<Setting, MongoSettingRepository>();
            options.AddRepository<SettingDefinitionRecord, MongoSettingDefinitionRecordRepository>();
        });
    }
}
