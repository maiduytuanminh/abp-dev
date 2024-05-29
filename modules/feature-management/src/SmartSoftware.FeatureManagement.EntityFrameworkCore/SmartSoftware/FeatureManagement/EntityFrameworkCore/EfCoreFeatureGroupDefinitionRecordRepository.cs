using System;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.FeatureManagement.EntityFrameworkCore;

public class EfCoreFeatureGroupDefinitionRecordRepository :
    EfCoreRepository<IFeatureManagementDbContext, FeatureGroupDefinitionRecord, Guid>,
    IFeatureGroupDefinitionRecordRepository
{
    public EfCoreFeatureGroupDefinitionRecordRepository(
        IDbContextProvider<IFeatureManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
