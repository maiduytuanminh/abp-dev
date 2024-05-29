using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Blogging.EntityFrameworkCore;

namespace SmartSoftware.Blogging.Blogs
{
    public class EfCoreBlogRepository : EfCoreRepository<IBloggingDbContext, Blog, Guid>, IBlogRepository
    {
        public EfCoreBlogRepository(IDbContextProvider<IBloggingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<Blog> FindByShortNameAsync(string shortName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(p => p.ShortName == shortName, GetCancellationToken(cancellationToken));
        }
    }
}
