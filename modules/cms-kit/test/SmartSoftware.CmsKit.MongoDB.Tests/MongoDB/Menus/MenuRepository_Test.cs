using Xunit;
using SmartSoftware.CmsKit.Menus;

namespace SmartSoftware.CmsKit.MongoDB.Menus;

[Collection(MongoTestCollection.Name)]
public class MenuRepository_Test : MenuItemRepository_Test<CmsKitMongoDbTestModule>
{

}
