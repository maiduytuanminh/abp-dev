using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Domain;

[Collection(MongoTestCollection.Name)]
public class EntityCache_Tests : EntityCache_Tests<SmartSoftwareMongoDbTestModule>
{
}