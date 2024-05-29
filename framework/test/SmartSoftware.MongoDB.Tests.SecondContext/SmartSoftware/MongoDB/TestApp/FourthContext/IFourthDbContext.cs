using MongoDB.Driver;
using SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;

namespace SmartSoftware.MongoDB.TestApp.FourthContext;

public interface IFourthDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<FourthDbContextDummyEntity> FourthDummyEntities { get; }
}
