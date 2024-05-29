using Xunit;

namespace SmartSoftware.OpenIddict.MongoDB;

[Collection(MongoTestCollection.Name)]
public class OpenIddictTokenRepository_Tests : OpenIddictTokenRepository_Tests<OpenIddictMongoDbTestModule>
{
    
}