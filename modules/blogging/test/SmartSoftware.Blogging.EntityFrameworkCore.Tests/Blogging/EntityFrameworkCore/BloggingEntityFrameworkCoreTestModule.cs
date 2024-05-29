using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SmartSoftware;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Sqlite;
using SmartSoftware.Modularity;

namespace SmartSoftware.Blogging.EntityFrameworkCore
{
    [DependsOn(
        typeof(BloggingEntityFrameworkCoreModule),
        typeof(BloggingTestBaseModule),
        typeof(SmartSoftwareEntityFrameworkCoreSqliteModule)
    )]
    public class BloggingEntityFrameworkCoreTestModule : SmartSoftwareModule
    {
        private SqliteConnection _sqliteConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            _sqliteConnection = CreateDatabaseAndGetConnection();

            Configure<SmartSoftwareDbContextOptions>(options =>
            {
                options.Configure(ssDbContextConfigurationContext =>
                {
                    ssDbContextConfigurationContext.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        private static SqliteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SmartSoftwareUnitTestSqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<BloggingDbContext>().UseSqlite(connection).Options;
            using (var context = new BloggingDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            _sqliteConnection.Dispose();
        }
    }
}