using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace SmartSoftware.TenantManagement.MongoDB;

[DependsOn(
    typeof(SmartSoftwareTenantManagementDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class SmartSoftwareTenantManagementMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<TenantManagementMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<ITenantManagementMongoDbContext>();

            options.AddRepository<Tenant, MongoTenantRepository>();
        });
    }
}
