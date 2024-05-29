using Microsoft.EntityFrameworkCore;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace BlobStoring.Database.Host.ConsoleApp.ConsoleApp.EfCore;

public class BlobStoringHostDbContext : SmartSoftwareDbContext<BlobStoringHostDbContext>
{
    public BlobStoringHostDbContext(DbContextOptions<BlobStoringHostDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBlobStoring();
    }
}
