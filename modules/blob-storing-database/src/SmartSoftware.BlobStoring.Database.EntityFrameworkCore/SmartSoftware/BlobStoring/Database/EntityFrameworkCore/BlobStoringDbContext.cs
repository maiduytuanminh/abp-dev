using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.BlobStoring.Database.EntityFrameworkCore;

[ConnectionStringName(SmartSoftwareBlobStoringDatabaseDbProperties.ConnectionStringName)]
public class BlobStoringDbContext : SmartSoftwareDbContext<BlobStoringDbContext>, IBlobStoringDbContext
{
    public DbSet<DatabaseBlobContainer> BlobContainers { get; set; }

    public DbSet<DatabaseBlob> Blobs { get; set; }

    public BlobStoringDbContext(DbContextOptions<BlobStoringDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBlobStoring();
    }
}
