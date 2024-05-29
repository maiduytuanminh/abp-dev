using Xunit;

namespace SmartSoftware.SettingManagement.MongoDB;

[Collection(MongoTestCollection.Name)]
public class SettingRepository_Tests : SettingRepository_Tests<SmartSoftwareSettingManagementMongoDbTestModule>
{

}
