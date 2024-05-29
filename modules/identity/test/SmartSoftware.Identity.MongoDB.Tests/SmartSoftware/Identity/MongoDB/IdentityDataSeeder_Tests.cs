using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class IdentityDataSeeder_Tests : IdentityDataSeeder_Tests<SmartSoftwareIdentityMongoDbTestModule>
{

}
