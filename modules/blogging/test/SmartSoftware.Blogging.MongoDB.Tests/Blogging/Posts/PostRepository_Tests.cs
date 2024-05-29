using SmartSoftware.Blogging.Posts;
using SmartSoftware.Blogging.MongoDB;
using Xunit;

namespace SmartSoftware.Blogging
{
    [Collection(MongoTestCollection.Name)]
    public class PostRepository_Tests : PostRepository_Tests<BloggingMongoDbTestModule>
    {
    }
}