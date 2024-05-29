using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareBackgroundJobsTestBaseModule),
    typeof(SmartSoftwareBackgroundJobsEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
    )]
public class SmartSoftwareBackgroundJobsEntityFrameworkCoreTestModule : SmartSoftwareModule
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
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new SmartSoftwareUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        new BackgroundJobsDbContext(
            new DbContextOptionsBuilder<BackgroundJobsDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
