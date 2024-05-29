using Xunit;

namespace SmartSoftware.PermissionManagement.MongoDB;

[Collection(MongoTestCollection.Name)]
public class MongoDbPermissionDefinitionRecordRepository_Tests : PermissionGrantRepository_Tests<SmartSoftwarePermissionManagementMongoDbTestModule>
{

}
