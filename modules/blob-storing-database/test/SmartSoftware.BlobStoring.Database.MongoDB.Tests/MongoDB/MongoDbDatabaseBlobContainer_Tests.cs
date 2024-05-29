using Xunit;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

[Collection(MongoTestCollection.Name)]
public class MongoDbDatabaseBlobContainer_Tests : DatabaseBlobContainer_Tests<BlobStoringDatabaseMongoDbTestModule>
{

}
