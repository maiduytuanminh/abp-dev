using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.SettingManagement.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareSettingManagementDomainModule),
    typeof(SmartSoftwareEntityFrameworkCoreModule)
    )]
public class SmartSoftwareSettingManagementEntityFrameworkCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<SettingManagementDbContext>(options =>
        {
            options.AddDefaultRepositories<ISettingManagementDbContext>();

            options.AddRepository<Setting, EfCoreSettingRepository>();
            options.AddRepository<SettingDefinitionRecord, EfCoreSettingDefinitionRecordRepository>();
        });
    }
}
