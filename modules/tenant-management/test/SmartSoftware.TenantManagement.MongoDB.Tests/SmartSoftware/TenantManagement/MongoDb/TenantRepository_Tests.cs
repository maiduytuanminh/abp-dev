using Xunit;

namespace SmartSoftware.TenantManagement.MongoDB;

[Collection(MongoTestCollection.Name)]
public class TenantRepository_Tests : TenantRepository_Tests<SmartSoftwareTenantManagementMongoDbTestModule>
{

}
