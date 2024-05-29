using System;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.MongoDB;

namespace SmartSoftware.MongoDB.TestApp.FifthContext;

public interface IFifthDbContextDummyEntityRepository : IBasicRepository<FifthDbContextDummyEntity, Guid>
{
    Task<ISmartSoftwareMongoDbContext> GetDbContextAsync();
}

public class FifthDbContextDummyEntityRepository :
    MongoDbRepository<IFifthDbContext, FifthDbContextDummyEntity, Guid>,
    IFifthDbContextDummyEntityRepository
{
    public FifthDbContextDummyEntityRepository(IMongoDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<ISmartSoftwareMongoDbContext> GetDbContextAsync()
    {
        return await base.GetDbContextAsync();
    }
}
