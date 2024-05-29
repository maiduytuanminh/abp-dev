using Xunit;

namespace SmartSoftware.FeatureManagement.MongoDB;

[Collection(MongoTestCollection.Name)]
public class FeatureValueRepositoryTests : FeatureValueRepository_Tests<SmartSoftwareFeatureManagementMongoDbTestModule>
{

}
