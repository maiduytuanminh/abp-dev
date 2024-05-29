using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class IdentityClaimTypeRepository_Tests : IdentityClaimTypeRepository_Tests<SmartSoftwareIdentityMongoDbTestModule>
{

}
