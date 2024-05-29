using Xunit;

namespace SmartSoftware.IdentityServer;

[Collection(MongoTestCollection.Name)]
public class IdentityResourceRepository_Tests : IdentityResourceRepository_Tests<SmartSoftwareIdentityServerMongoDbTestModule>
{
}
