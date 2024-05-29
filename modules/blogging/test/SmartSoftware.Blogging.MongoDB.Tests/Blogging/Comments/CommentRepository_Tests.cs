using SmartSoftware.Blogging.Comments;
using SmartSoftware.Blogging.MongoDB;
using Xunit;

namespace SmartSoftware.Blogging
{
    [Collection(MongoTestCollection.Name)]
    public class CommentRepository_Tests : CommentRepository_Tests<BloggingMongoDbTestModule>
    {
    }
}