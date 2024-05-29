using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Identity.AspNetCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.Threading;

namespace SmartSoftware.Account;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAuthorizationModule),
    typeof(SmartSoftwareIdentityAspNetCoreModule),
    typeof(SmartSoftwareAccountApplicationModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
)]
public class SmartSoftwareAccountApplicationTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();

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

        new IdentityDbContext(
            new DbContextOptionsBuilder<IdentityDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        new PermissionManagementDbContext(
            new DbContextOptionsBuilder<PermissionManagementDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();


        return connection;
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            AsyncHelper.RunSync(async () =>
            {
                await dataSeeder.SeedAsync();
                await scope.ServiceProvider
                    .GetRequiredService<SmartSoftwareAccountTestDataBuilder>()
                    .Build();
            });
        }
    }
}
