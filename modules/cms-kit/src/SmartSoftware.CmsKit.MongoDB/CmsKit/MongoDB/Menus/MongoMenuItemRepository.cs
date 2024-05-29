using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;
using SmartSoftware.CmsKit.Menus;

namespace SmartSoftware.CmsKit.MongoDB.Menus;

public class MongoMenuItemRepository : MongoDbRepository<ICmsKitMongoDbContext, MenuItem, Guid>, IMenuItemRepository
{
    public MongoMenuItemRepository(IMongoDbContextProvider<ICmsKitMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
