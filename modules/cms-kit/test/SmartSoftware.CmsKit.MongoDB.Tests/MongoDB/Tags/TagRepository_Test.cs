using SmartSoftware.CmsKit.Tags;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Tags;

[Collection(MongoTestCollection.Name)]
public class TagRepository_Test : TagRepository_Test<CmsKitMongoDbTestModule>
{

}
