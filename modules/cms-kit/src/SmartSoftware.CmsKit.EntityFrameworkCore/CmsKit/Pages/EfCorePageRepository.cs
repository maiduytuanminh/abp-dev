using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartSoftware;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.Pages;

public class EfCorePageRepository : EfCoreRepository<ICmsKitDbContext, Page, Guid>, IPageRepository
{
    public EfCorePageRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<int> GetCountAsync(string filter = null,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).WhereIf(
            !filter.IsNullOrWhiteSpace(),
            x =>
                x.Title.ToLower().Contains(filter.ToLower()) || x.Slug.Contains(filter)
        ).CountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Page>> GetListAsync(
        string filter = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        string sorting = null,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).WhereIf(
                !filter.IsNullOrWhiteSpace(),
                x =>
                    x.Title.ToLower().Contains(filter.ToLower()) || x.Slug.Contains(filter))
            .OrderBy(sorting.IsNullOrEmpty() ? nameof(Page.Title) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual Task<Page> GetBySlugAsync([NotNull] string slug, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(slug, nameof(slug));
        return GetAsync(x => x.Slug == slug, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual Task<Page> FindBySlugAsync([NotNull] string slug, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(slug, nameof(slug));
        return FindAsync(x => x.Slug == slug, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> ExistsAsync([NotNull] string slug,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(slug, nameof(slug));
        return await (await GetDbSetAsync()).AnyAsync(x => x.Slug == slug, GetCancellationToken(cancellationToken));
    }

    public virtual Task<List<Page>> GetListOfHomePagesAsync(CancellationToken cancellationToken = default)
    {
        return GetListAsync(x => x.IsHomePage, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public async Task<string?> FindTitleAsync(Guid pageId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).Where(x => x.Id == pageId).Select(x => x.Title).FirstOrDefaultAsync(cancellationToken);
    }
}
