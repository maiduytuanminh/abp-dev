using SmartSoftware.MongoDB;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

public static class BlobStoringMongoDbContextExtensions
{
    public static void ConfigureBlobStoring(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<DatabaseBlobContainer>(b =>
        {
            b.CollectionName = SmartSoftwareBlobStoringDatabaseDbProperties.DbTablePrefix + "BlobContainers";
        });

        builder.Entity<DatabaseBlob>(b =>
        {
            b.CollectionName = SmartSoftwareBlobStoringDatabaseDbProperties.DbTablePrefix + "Blobs";
        });
    }
}
