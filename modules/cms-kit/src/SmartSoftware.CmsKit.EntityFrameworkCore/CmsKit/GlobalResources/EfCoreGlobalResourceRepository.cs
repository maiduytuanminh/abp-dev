using System;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.GlobalResources;

public class EfCoreGlobalResourceRepository: EfCoreRepository<ICmsKitDbContext, GlobalResource, Guid>, IGlobalResourceRepository
{
    public EfCoreGlobalResourceRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
        
    }

    public Task<GlobalResource> FindByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(name, nameof(name));
        return FindAsync(x => x.Name == name, cancellationToken: GetCancellationToken(cancellationToken));
    }
}