using System;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;

namespace SmartSoftware.BlobStoring.Database.MongoDB;

public class MongoDbDatabaseBlobContainerRepository : MongoDbRepository<IBlobStoringMongoDbContext, DatabaseBlobContainer, Guid>, IDatabaseBlobContainerRepository
{
    public MongoDbDatabaseBlobContainerRepository(IMongoDbContextProvider<IBlobStoringMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public virtual async Task<DatabaseBlobContainer> FindAsync(string name, CancellationToken cancellationToken = default)
    {
        return await base.FindAsync(x => x.Name == name, cancellationToken: GetCancellationToken(cancellationToken));
    }
}
