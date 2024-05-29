using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.MongoDB.TestApp.FourthContext;
using SmartSoftware.MongoDB.TestApp.SecondContext;
using SmartSoftware.MongoDB.TestApp.ThirdDbContext;
using SmartSoftware.TestApp.MongoDB;
using Xunit;

namespace SmartSoftware.MongoDB;

[Collection(MongoTestCollection.Name)]
public class SmartSoftwareMongoDbConventionalRegistrar_Tests : MongoDbTestBase
{
    [Fact]
    public void All_SmartSoftwareMongoDbContext_Should_Exposed_ISmartSoftwareMongoDbContext_Service()
    {
        var ssMongoDbContext = ServiceProvider.GetServices<ISmartSoftwareMongoDbContext>();
        ssMongoDbContext.ShouldContain(x => x is TestAppMongoDbContext);
        ssMongoDbContext.ShouldContain(x => x is SecondDbContext);
        ssMongoDbContext.ShouldContain(x => x is ThirdDbContext);
        ssMongoDbContext.ShouldContain(x => x is FourthDbContext);
    }
}
