using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.FeatureManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwareFeatureManagementMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<FeatureManagementMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<IFeatureManagementMongoDbContext>();

            options.AddRepository<FeatureGroupDefinitionRecord, MongoFeatureGroupDefinitionRecordRepository>();
            options.AddRepository<FeatureDefinitionRecord, MongoFeatureDefinitionRecordRepository>();
            options.AddRepository<FeatureValue, MongoFeatureValueRepository>();
        });
    }
}
