using SmartSoftware.CmsKit.Pages;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Pages;

[Collection(MongoTestCollection.Name)]
public class PageRepository_Test : PageRepository_Test<CmsKitMongoDbTestModule>
{

}
