using SmartSoftware.MongoDB;
using SmartSoftware.Users.MongoDB;
using SmartSoftware.CmsKit.Users;

namespace SmartSoftware.CmsKit.MongoDB.Users;

public class MongoCmsUserRepository : MongoUserRepositoryBase<ICmsKitMongoDbContext, CmsUser>, ICmsUserRepository
{
    public MongoCmsUserRepository(IMongoDbContextProvider<ICmsKitMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
