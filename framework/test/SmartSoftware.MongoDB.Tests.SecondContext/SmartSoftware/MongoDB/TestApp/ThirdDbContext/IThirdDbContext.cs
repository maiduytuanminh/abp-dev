using MongoDB.Driver;

namespace SmartSoftware.MongoDB.TestApp.ThirdDbContext;

public interface IThirdDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<ThirdDbContextDummyEntity> DummyEntities { get; }
}
