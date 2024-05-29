using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class Repository_Queryable_Tests : Repository_Queryable_Tests<SmartSoftwareMongoDbTestModule>
{

}
