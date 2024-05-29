using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class SoftDelete_Filter_Tests : SoftDelete_Filter_Tests<SmartSoftwareMongoDbTestModule>
{

}
