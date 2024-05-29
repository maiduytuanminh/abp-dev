using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Auditing;

[Collection(MongoTestCollection.Name)]
public class Auditing_Tests : Auditing_Tests<SmartSoftwareMongoDbTestModule>
{

}
