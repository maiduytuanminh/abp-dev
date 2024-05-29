using MongoDB.Driver;

namespace SmartSoftware.MongoDB;

public interface ISmartSoftwareMongoDbContext
{
    IMongoClient Client { get; }

    IMongoDatabase Database { get; }

    IMongoCollection<T> Collection<T>();

    IClientSessionHandle? SessionHandle { get; }
}
