using SmartSoftware.Blogging.Tagging;
using SmartSoftware.Blogging.MongoDB;
using Xunit;

namespace SmartSoftware.Blogging
{
    [Collection(MongoTestCollection.Name)]
    public class TagRepository_Tests : TagRepository_Tests<BloggingMongoDbTestModule>
    {
    }
}