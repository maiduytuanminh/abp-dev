using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Uow;

[Collection(MongoTestCollection.Name)]
public class Uow_Completed_Tests : Uow_Completed_Tests<SmartSoftwareMongoDbTestModule>
{
}
