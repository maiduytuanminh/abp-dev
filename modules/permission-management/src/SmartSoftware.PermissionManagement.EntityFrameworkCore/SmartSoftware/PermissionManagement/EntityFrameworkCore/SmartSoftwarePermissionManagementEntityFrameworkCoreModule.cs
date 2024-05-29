using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement.EntityFrameworkCore;

[DependsOn(typeof(SmartSoftwarePermissionManagementDomainModule))]
[DependsOn(typeof(SmartSoftwareEntityFrameworkCoreModule))]
public class SmartSoftwarePermissionManagementEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<PermissionManagementDbContext>(options =>
        {
            options.AddDefaultRepositories<IPermissionManagementDbContext>();

            options.AddRepository<PermissionGroupDefinitionRecord, EfCorePermissionGroupDefinitionRecordRepository>();
            options.AddRepository<PermissionDefinitionRecord, EfCorePermissionDefinitionRecordRepository>();
            options.AddRepository<PermissionGrant, EfCorePermissionGrantRepository>();
        });
    }
}
