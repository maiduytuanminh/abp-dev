using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SmartSoftwareDocs.EntityFrameworkCore
{
    public class SmartSoftwareDocsDbContextFactory : IDesignTimeDbContextFactory<SmartSoftwareDocsDbContext>
    {
        public SmartSoftwareDocsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SmartSoftwareDocsDbContext>()
                .UseSqlServer(configuration["ConnectionString"]);

            return new SmartSoftwareDocsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SmartSoftwareDocs.Web/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
