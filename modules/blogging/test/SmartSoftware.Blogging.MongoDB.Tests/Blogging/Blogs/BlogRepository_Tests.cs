using SmartSoftware.Blogging.Blogs;
using SmartSoftware.Blogging.MongoDB;
using Xunit;

namespace SmartSoftware.Blogging
{
    [Collection(MongoTestCollection.Name)]
    public class BlogRepository_Tests : BlogRepository_Tests<BloggingMongoDbTestModule>
    {
    }
}