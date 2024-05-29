using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

[ConnectionStringName(SmartSoftwareBlobStoringDatabaseDbProperties.ConnectionStringName)]
public interface IBlobStoringMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<DatabaseBlobContainer> BlobContainers { get; }

    IMongoCollection<DatabaseBlob> Blobs { get; }
}
