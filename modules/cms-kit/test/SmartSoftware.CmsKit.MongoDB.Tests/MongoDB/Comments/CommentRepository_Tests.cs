using SmartSoftware.CmsKit.Comments;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Comments;

[Collection(MongoTestCollection.Name)]
public class CommentRepository_Tests : CommentRepository_Tests<CmsKitMongoDbTestModule>
{

}
