using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.Uow;

namespace SmartSoftware.Identity.EntityFrameworkCore;

[DependsOn(
    typeof(SmartSoftwareIdentityTestBaseModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
    )]
public class SmartSoftwareIdentityEntityFrameworkCoreTestModule : SmartSoftwareModule
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

        new IdentityDbContext(
            new DbContextOptionsBuilder<IdentityDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        new PermissionManagementDbContext(
            new DbContextOptionsBuilder<PermissionManagementDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
