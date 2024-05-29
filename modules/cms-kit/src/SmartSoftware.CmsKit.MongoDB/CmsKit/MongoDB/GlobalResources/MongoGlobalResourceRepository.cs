using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SmartSoftware;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;
using SmartSoftware.CmsKit.GlobalResources;

namespace SmartSoftware.CmsKit.MongoDB.GlobalResources;

public class MongoGlobalResourceRepository: MongoDbRepository<ICmsKitMongoDbContext, GlobalResource, Guid>, IGlobalResourceRepository
{
    public MongoGlobalResourceRepository(IMongoDbContextProvider<ICmsKitMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public Task<GlobalResource> FindByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(name, nameof(name));
        return FindAsync(x => x.Name == name, cancellationToken: GetCancellationToken(cancellationToken));
    }
}