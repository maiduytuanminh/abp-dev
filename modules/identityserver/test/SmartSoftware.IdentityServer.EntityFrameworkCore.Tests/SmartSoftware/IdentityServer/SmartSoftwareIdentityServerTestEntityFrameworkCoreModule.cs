using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.IdentityServer.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.Threading;
using SmartSoftware.Uow;

namespace SmartSoftware.IdentityServer;

[DependsOn(
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityServerEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityServerTestBaseModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule)
    )]
public class SmartSoftwareIdentityServerTestEntityFrameworkCoreModule : SmartSoftwareModule
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

        new IdentityServerDbContext(
            new DbContextOptionsBuilder<IdentityServerDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        new PermissionManagementDbContext(
            new DbContextOptionsBuilder<PermissionManagementDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
