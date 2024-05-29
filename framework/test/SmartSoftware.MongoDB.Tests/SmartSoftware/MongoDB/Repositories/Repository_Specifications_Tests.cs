using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class Repository_Specifications_Tests : Repository_Specifications_Tests<SmartSoftwareMongoDbTestModule>
{
}
