using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;

namespace SmartSoftware.TestApp.MongoDB;

[ConnectionStringName("TestApp")]
public interface ITestAppMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<Person> People { get; }

    IMongoCollection<City> Cities { get; }

    IMongoCollection<Product> Products { get; }

    IMongoCollection<AppEntityWithNavigations> AppEntityWithNavigations { get; }
}
