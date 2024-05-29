using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class IdentityLinkUserRepository_Tests : IdentityLinkUserRepository_Tests<SmartSoftwareIdentityMongoDbTestModule>
{

}
