using Xunit;

namespace SmartSoftware.IdentityServer;

[Collection(MongoTestCollection.Name)]
public class PersistentGrantRepository_Tests : PersistentGrantRepository_Tests<SmartSoftwareIdentityServerMongoDbTestModule>
{

}
