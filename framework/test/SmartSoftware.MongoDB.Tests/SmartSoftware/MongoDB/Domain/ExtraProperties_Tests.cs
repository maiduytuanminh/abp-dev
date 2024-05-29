using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class ExtraProperties_Tests : ExtraProperties_Tests<SmartSoftwareMongoDbTestModule>
{

}
