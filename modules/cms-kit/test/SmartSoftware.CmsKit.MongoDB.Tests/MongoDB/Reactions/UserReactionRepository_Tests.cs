using SmartSoftware.CmsKit.Reactions;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Reactions;

[Collection(MongoTestCollection.Name)]
public class UserReactionRepository_Tests : UserReactionRepository_Tests<CmsKitMongoDbTestModule>
{
}
