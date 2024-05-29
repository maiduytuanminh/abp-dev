using System;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.MongoDB;

namespace SmartSoftware.MongoDB.TestApp.FifthContext;

public interface IFifthDbContextMultiTenantDummyEntityRepository : IBasicRepository<FifthDbContextMultiTenantDummyEntity, Guid>
{
    Task<ISmartSoftwareMongoDbContext> GetDbContextAsync();
}

public class FifthDbContextMultiTenantDummyEntityRepository :
    MongoDbRepository<IFifthDbContext, FifthDbContextMultiTenantDummyEntity, Guid>,
    IFifthDbContextMultiTenantDummyEntityRepository
{
    public FifthDbContextMultiTenantDummyEntityRepository(IMongoDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<ISmartSoftwareMongoDbContext> GetDbContextAsync()
    {
        return await base.GetDbContextAsync();
    }
}
