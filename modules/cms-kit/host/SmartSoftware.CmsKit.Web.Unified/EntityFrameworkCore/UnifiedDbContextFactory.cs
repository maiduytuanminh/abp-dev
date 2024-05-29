#if EntityFrameworkCore
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SmartSoftware.CmsKit.EntityFrameworkCore;

public class UnifiedDbContextFactory : IDesignTimeDbContextFactory<UnifiedDbContext>
{
    public UnifiedDbContext CreateDbContext(string[] args)
    {
        FeatureConfigurer.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<UnifiedDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new UnifiedDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
#endif