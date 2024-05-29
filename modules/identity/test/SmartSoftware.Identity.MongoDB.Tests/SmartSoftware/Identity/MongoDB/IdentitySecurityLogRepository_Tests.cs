using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class IdentitySecurityLogRepository_Tests : IdentitySecurityLogRepository_Tests<SmartSoftwareIdentityMongoDbTestModule>
{

}
