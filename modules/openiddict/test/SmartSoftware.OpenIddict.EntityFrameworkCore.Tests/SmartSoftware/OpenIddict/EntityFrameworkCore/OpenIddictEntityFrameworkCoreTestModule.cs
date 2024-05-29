using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.Uow;

namespace SmartSoftware.OpenIddict.EntityFrameworkCore;

[DependsOn(
    typeof(OpenIddictTestBaseModule),
    typeof(SmartSoftwareOpenIddictEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule)
    )]
public class OpenIddictEntityFrameworkCoreTestModule : SmartSoftwareModule
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

        new OpenIddictDbContext(
            new DbContextOptionsBuilder<OpenIddictDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        new PermissionManagementDbContext(
            new DbContextOptionsBuilder<PermissionManagementDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
