using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DomainEvents;

[Collection(MongoTestCollection.Name)]
public class EntityChangeEvents_Tests : EntityChangeEvents_Tests<SmartSoftwareMongoDbTestModule>
{

}
