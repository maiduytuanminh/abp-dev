using Xunit;

namespace SmartSoftware.IdentityServer;

[Collection(MongoTestCollection.Name)]
public class ApiResourceRepository_Tests : ApiResourceRepository_Tests<SmartSoftwareIdentityServerMongoDbTestModule>
{
}
