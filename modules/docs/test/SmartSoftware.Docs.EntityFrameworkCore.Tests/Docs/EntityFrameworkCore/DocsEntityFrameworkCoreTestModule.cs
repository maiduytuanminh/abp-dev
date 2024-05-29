using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;

namespace SmartSoftware.Docs.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocsTestBaseModule),
        typeof(DocsEntityFrameworkCoreModule),
        typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
        )]
    public class DocsEntityFrameworkCoreTestModule : SmartSoftwareModule
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

            new DocsDbContext(
                new DbContextOptionsBuilder<DocsDbContext>().UseSqlite(connection).Options
            ).GetService<IRelationalDatabaseCreator>().CreateTables();
            
            return connection;
        }
    }
}
