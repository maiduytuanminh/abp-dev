using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

[ConnectionStringName(SmartSoftwareBlobStoringDatabaseDbProperties.ConnectionStringName)]
public class BlobStoringMongoDbContext : SmartSoftwareMongoDbContext, IBlobStoringMongoDbContext
{
    public IMongoCollection<DatabaseBlobContainer> BlobContainers => Collection<DatabaseBlobContainer>();

    public IMongoCollection<DatabaseBlob> Blobs => Collection<DatabaseBlob>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureBlobStoring();
    }
}
