using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSoftware.MongoDB;

public interface IMongoDbContextProvider<TMongoDbContext>
    where TMongoDbContext : ISmartSoftwareMongoDbContext
{
    [Obsolete("Use CreateDbContextAsync")]
    TMongoDbContext GetDbContext();

    Task<TMongoDbContext> GetDbContextAsync(CancellationToken cancellationToken = default);
}
