using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class IdentityRoleRepository_Tests : IdentityRoleRepository_Tests<SmartSoftwareIdentityMongoDbTestModule>
{

}
