using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Timing;

namespace SmartSoftware.BackgroundJobs.EntityFrameworkCore;

public class EfCoreBackgroundJobRepository : EfCoreRepository<IBackgroundJobsDbContext, BackgroundJobRecord, Guid>, IBackgroundJobRepository
{
    protected IClock Clock { get; }

    public EfCoreBackgroundJobRepository(
        IDbContextProvider<IBackgroundJobsDbContext> dbContextProvider,
        IClock clock)
        : base(dbContextProvider)
    {
        Clock = clock;
    }

    public virtual async Task<List<BackgroundJobRecord>> GetWaitingListAsync(
        int maxResultCount,
        CancellationToken cancellationToken = default)
    {
        return await (await GetWaitingListQueryAsync(maxResultCount)).ToListAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<BackgroundJobRecord>> GetWaitingListQueryAsync(int maxResultCount)
    {
        var now = Clock.Now;
        return (await GetDbSetAsync())
            .Where(t => !t.IsAbandoned && t.NextTryTime <= now)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.TryCount)
            .ThenBy(t => t.NextTryTime)
            .Take(maxResultCount);
    }
}
