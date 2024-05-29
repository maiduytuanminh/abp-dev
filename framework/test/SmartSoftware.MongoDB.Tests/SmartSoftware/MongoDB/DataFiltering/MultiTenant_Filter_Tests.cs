using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class MultiTenant_Filter_Tests : MultiTenant_Filter_Tests<SmartSoftwareMongoDbTestModule>
{

}
