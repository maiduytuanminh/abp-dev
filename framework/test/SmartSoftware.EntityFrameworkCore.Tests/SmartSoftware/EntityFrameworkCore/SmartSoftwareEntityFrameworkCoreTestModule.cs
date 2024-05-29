using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.EntityFrameworkCore.Domain;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;
using SmartSoftware.EntityFrameworkCore.TestApp.SecondContext;
using SmartSoftware.EntityFrameworkCore.TestApp.ThirdDbContext;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.TestApp;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.EntityFrameworkCore;
using SmartSoftware.Threading;

namespace SmartSoftware.EntityFrameworkCore;

[DependsOn(typeof(SmartSoftwareEntityFrameworkCoreSqliteModule))]
[DependsOn(typeof(TestAppModule))]
[DependsOn(typeof(SmartSoftwareAutofacModule))]
[DependsOn(typeof(SmartSoftwareEfCoreTestSecondContextModule))]
public class SmartSoftwareEntityFrameworkCoreTestModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        TestEntityExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSmartSoftwareDbContext<TestAppDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.ReplaceDbContext<IThirdDbContext>();

            options.Entity<Person>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.Phones);
            });

            options.Entity<Author>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.Books);
            });

            options.Entity<AppEntityWithNavigations>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.OneToOne).ThenInclude(x => x.OneToOne).Include(p => p.OneToMany).ThenInclude(x => x.OneToMany).Include(p => p.ManyToMany);
            });
        });

        context.Services.AddSmartSoftwareDbContext<HostTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.ReplaceDbContext<IFifthDbContext>(MultiTenancySides.Host);
        });

        context.Services.AddSmartSoftwareDbContext<TenantTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.Configure(ssDbContextConfigurationContext =>
            {
                ssDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        context.ServiceProvider.GetRequiredService<SecondDbContext>().Database.Migrate();

        using (var scope = context.ServiceProvider.CreateScope())
        {
            var categoryRepository = scope.ServiceProvider.GetRequiredService<IBasicRepository<Category, Guid>>();
            AsyncHelper.RunSync(async () =>
            {
                await categoryRepository.InsertManyAsync(new List<Category>
                {
                    new Category { Name = "smartsoftware" },
                    new Category { Name = "ss.cli" },
                    new Category { Name = "ss.core", IsDeleted = true }
                });
            });
        }
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new SmartSoftwareUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        using (var context = new TestMigrationsDbContext(new DbContextOptionsBuilder<TestMigrationsDbContext>().UseSqlite(connection).Options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
            context.Database.ExecuteSqlRaw(
                @"CREATE VIEW View_PersonView AS 
                      SELECT Name, CreationTime, Birthday, LastActive FROM People");
        }

        return connection;
    }
}
