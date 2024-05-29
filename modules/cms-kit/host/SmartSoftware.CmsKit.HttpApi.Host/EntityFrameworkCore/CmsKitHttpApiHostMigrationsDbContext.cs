using Microsoft.EntityFrameworkCore;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.EntityFrameworkCore;

public class CmsKitHttpApiHostMigrationsDbContext : SmartSoftwareDbContext<CmsKitHttpApiHostMigrationsDbContext>
{
    public CmsKitHttpApiHostMigrationsDbContext(DbContextOptions<CmsKitHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCmsKit();
        modelBuilder.ConfigureBlobStoring();
    }
}
