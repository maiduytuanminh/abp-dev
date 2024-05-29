using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftwareDocs.EntityFrameworkCore;

namespace SmartSoftwareDocs.Migrator
{
    [DependsOn(typeof(SmartSoftwareDocsEntityFrameworkCoreModule))]
    public class SmartSoftwareDocsMigratorModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddSmartSoftwareDbContext<SmartSoftwareDocsDbContext>();

            Configure<SmartSoftwareDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = configuration["ConnectionString"];
            });

            Configure<SmartSoftwareDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });
        }
    }
}