using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class ConcurrencyStamp_Tests : ConcurrencyStamp_Tests<SmartSoftwareMongoDbTestModule>
{

}
