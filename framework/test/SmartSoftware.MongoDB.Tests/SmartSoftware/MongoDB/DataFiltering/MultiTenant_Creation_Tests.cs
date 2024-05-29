using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class MultiTenant_Creation_Tests : MultiTenant_Creation_Tests<SmartSoftwareMongoDbTestModule>
{

}
