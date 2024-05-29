using System;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;

public interface IFifthDbContextDummyEntityRepository : IBasicRepository<FifthDbContextDummyEntity, Guid>
{

}

public class FifthDbContextDummyEntityRepository :
    EfCoreRepository<IFifthDbContext, FifthDbContextDummyEntity, Guid>,
    IFifthDbContextDummyEntityRepository
{
    public FifthDbContextDummyEntityRepository(IDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
