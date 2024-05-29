using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.BlobStoring.Database.EntityFrameworkCore;

public class EfCoreDatabaseBlobContainerRepository : EfCoreRepository<IBlobStoringDbContext, DatabaseBlobContainer, Guid>, IDatabaseBlobContainerRepository
{
    public EfCoreDatabaseBlobContainerRepository(IDbContextProvider<IBlobStoringDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public virtual async Task<DatabaseBlobContainer> FindAsync(string name, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .FirstOrDefaultAsync(x => x.Name == name, GetCancellationToken(cancellationToken));
    }
}
