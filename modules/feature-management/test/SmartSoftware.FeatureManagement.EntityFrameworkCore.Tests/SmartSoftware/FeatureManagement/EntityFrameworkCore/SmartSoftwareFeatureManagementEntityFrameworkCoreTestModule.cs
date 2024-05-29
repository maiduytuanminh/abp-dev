using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.Uow;

namespace SmartSoftware.FeatureManagement.EntityFrameworkCore;

[DependsOn(
    typeof(FeatureManagementTestBaseModule),
    typeof(SmartSoftwareFeatureManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
    )]
public class SmartSoftwareFeatureManagementEntityFrameworkCoreTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.Configure(ssDbContextConfigurationContext =>
            {
                ssDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });

        context.Services.AddAlwaysDisableUnitOfWorkTransaction();
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new SmartSoftwareUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        new FeatureManagementDbContext(
            new DbContextOptionsBuilder<FeatureManagementDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var task = context.ServiceProvider.GetRequiredService<SmartSoftwareFeatureManagementDomainModule>().GetInitializeDynamicFeaturesTask();
        if (!task.IsCompleted)
        {
            AsyncHelper.RunSync(() => Awaited(task));
        }
    }

    private async static Task Awaited(Task task)
    {
        await task;
    }
}
