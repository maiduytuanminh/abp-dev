using BlobStoring.Database.Host.ConsoleApp.ConsoleApp.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.BlobStoring;
using SmartSoftware.BlobStoring.Database;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.Modularity;

namespace BlobStoring.Database.Host.ConsoleApp.ConsoleApp;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
)]
public class ConsoleAppConsoleAppModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureEntityFramework(context);

        context.Services.AddSingleton<IBlobProvider, DatabaseBlobProvider>();

        Configure<SmartSoftwareBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.ProviderType = typeof(DatabaseBlobProvider);
            });
        });
    }

    private void ConfigureEntityFramework(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = "Server=localhost;Database=BlobStoring_Host;Trusted_Connection=True";
        });

        context.Services.AddSmartSoftwareDbContext<BlobStoringHostDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        Configure<SmartSoftwareDbContextOptions>(x =>
        {
            x.UseSqlServer();
        });
    }
}
