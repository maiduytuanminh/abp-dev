using System;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.CmsKit.EntityFrameworkCore;

namespace SmartSoftware.CmsKit.Menus;

public class EfCoreMenuItemRepository : EfCoreRepository<ICmsKitDbContext, MenuItem, Guid>, IMenuItemRepository
{
    public EfCoreMenuItemRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
