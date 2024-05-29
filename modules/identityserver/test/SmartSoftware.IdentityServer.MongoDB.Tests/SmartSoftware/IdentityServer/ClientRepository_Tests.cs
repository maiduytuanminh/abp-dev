using Xunit;

namespace SmartSoftware.IdentityServer;

[Collection(MongoTestCollection.Name)]
public class ClientRepository_Tests : ClientRepository_Tests<SmartSoftwareIdentityServerMongoDbTestModule>
{

}
