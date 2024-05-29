using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.BlobStoring.Database.EntityFrameworkCore;

[ConnectionStringName(SmartSoftwareBlobStoringDatabaseDbProperties.ConnectionStringName)]
public interface IBlobStoringDbContext : IEfCoreDbContext
{
    DbSet<DatabaseBlobContainer> BlobContainers { get; }

    DbSet<DatabaseBlob> Blobs { get; }
}
