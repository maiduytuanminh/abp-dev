using Xunit;

namespace SmartSoftware.OpenIddict.MongoDB;

[Collection(MongoTestCollection.Name)]
public class OpenIddictApplicationRepository_Tests : OpenIddictApplicationRepository_Tests<OpenIddictMongoDbTestModule>
{
    
}