using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;

namespace SmartSoftware.AuditLogging.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareAuditLoggingTestBaseModule),
    typeof(SmartSoftwareAuditLoggingEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
)]
public class SmartSoftwareAuditLoggingEntityFrameworkCoreTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.Configure(ctx =>
            {
                ctx.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new SmartSoftwareUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        new SmartSoftwareAuditLoggingDbContext(
            new DbContextOptionsBuilder<SmartSoftwareAuditLoggingDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
