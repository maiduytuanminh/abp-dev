using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Users.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.Users;

public class EfCoreCmsUserRepository : EfCoreUserRepositoryBase<ICmsKitDbContext, CmsUser>, ICmsUserRepository
{
    public EfCoreCmsUserRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
