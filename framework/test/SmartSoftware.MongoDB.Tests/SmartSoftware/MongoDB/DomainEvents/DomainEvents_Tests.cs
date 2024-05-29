using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DomainEvents;

[Collection(MongoTestCollection.Name)]
public class DomainEvents_Tests : DomainEvents_Tests<SmartSoftwareMongoDbTestModule>
{
}
