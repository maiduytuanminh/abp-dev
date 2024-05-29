using Xunit;

namespace SmartSoftware.FeatureManagement.MongoDB;

[Collection(MongoTestCollection.Name)]
public class FeatureManagementStore_Tests : FeatureManagementStore_Tests<SmartSoftwareFeatureManagementMongoDbTestModule>
{

}
