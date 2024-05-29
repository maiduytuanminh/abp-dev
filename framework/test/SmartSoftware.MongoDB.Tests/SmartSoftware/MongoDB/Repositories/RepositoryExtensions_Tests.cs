using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class RepositoryExtensions_Tests : RepositoryExtensions_Tests<SmartSoftwareMongoDbTestModule>
{

}
