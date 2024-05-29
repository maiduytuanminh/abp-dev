using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class IdentityUserRepository_Tests : IdentityUserRepository_Tests<SmartSoftwareIdentityMongoDbTestModule>
{

}
