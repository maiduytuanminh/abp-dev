using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Auditing.App.Entities;
using SmartSoftware.Auditing.App.EntityFrameworkCore;
using SmartSoftware.Autofac;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;

namespace SmartSoftware.Auditing;

[DependsOn(
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
)]
public class SmartSoftwareAuditingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<SmartSoftwareAuditingTestDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.Entity<AppEntityWithNavigations>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.OneToOne).Include(p => p.OneToMany).Include(p => p.ManyToMany);
            });
        });

        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.Configure(ssDbContextConfigurationContext =>
            {
                ssDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });

        Configure<SmartSoftwareAuditingOptions>(options =>
        {
            options.EntityHistorySelectors.Add(
                new NamedTypeSelector(
                    "AppEntityWithSelector",
                    type => type == typeof(AppEntityWithSelector))
            );

            options.EntityHistorySelectors.Add(
                new NamedTypeSelector(
                    "AppEntityWithSoftDelete",
                    type => type == typeof(AppEntityWithSoftDelete))
            );

            options.EntityHistorySelectors.Add(
                new NamedTypeSelector(
                    "AppEntityWithValueObject",
                    type => type == typeof(AppEntityWithValueObject) || type == typeof(AppEntityWithValueObjectAddress))
            );
        });

        context.Services.AddType<Auditing_Tests.MyAuditedObject1>();
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new SmartSoftwareUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        using (var context = new SmartSoftwareAuditingTestDbContext(new DbContextOptionsBuilder<SmartSoftwareAuditingTestDbContext>()
            .UseSqlite(connection).Options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }

        return connection;
    }
}
