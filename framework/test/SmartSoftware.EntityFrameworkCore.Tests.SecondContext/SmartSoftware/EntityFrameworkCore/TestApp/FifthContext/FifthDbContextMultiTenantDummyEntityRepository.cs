using System;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;

public interface IFifthDbContextMultiTenantDummyEntityRepository : IBasicRepository<FifthDbContextMultiTenantDummyEntity, Guid>
{

}

public class FifthDbContextMultiTenantDummyEntityRepository :
    EfCoreRepository<IFifthDbContext, FifthDbContextMultiTenantDummyEntity, Guid>,
    IFifthDbContextMultiTenantDummyEntityRepository
{
    public FifthDbContextMultiTenantDummyEntityRepository(IDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
