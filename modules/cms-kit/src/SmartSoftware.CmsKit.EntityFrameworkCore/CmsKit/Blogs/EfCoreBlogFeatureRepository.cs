using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.Blogs;

public class EfCoreBlogFeatureRepository : EfCoreRepository<ICmsKitDbContext, BlogFeature, Guid>, IBlogFeatureRepository
{
    public EfCoreBlogFeatureRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public Task<BlogFeature> FindAsync(Guid blogId, string featureName, CancellationToken cancellationToken = default)
    {
        return base.FindAsync(x => x.BlogId == blogId && x.FeatureName == featureName, cancellationToken: cancellationToken);
    }

    public virtual async Task<List<BlogFeature>> GetListAsync(Guid blogId, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
                        .Where(x => x.BlogId == blogId)
                        .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<BlogFeature>> GetListAsync(Guid blogId, List<string> featureNames, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
                    .Where(x => x.BlogId == blogId && featureNames.Contains(x.FeatureName))
                    .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
