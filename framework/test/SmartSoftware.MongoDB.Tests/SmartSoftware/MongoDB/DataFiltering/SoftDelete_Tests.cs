using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class SoftDelete_Tests : SoftDelete_Tests<SmartSoftwareMongoDbTestModule>
{

}
