using Xunit;

namespace SmartSoftware.PermissionManagement.MongoDB;

[Collection(MongoTestCollection.Name)]
public class PermissionGrantRepository_Tests : PermissionGrantRepository_Tests<SmartSoftwarePermissionManagementMongoDbTestModule>
{

}
