using SmartSoftware.Docs.MongoDB;
using Xunit;

namespace SmartSoftware.Docs.Project
{
    [Collection(MongoTestCollection.Name)]
    public class ProjectRepository_Tests : ProjectRepository_Tests<DocsMongoDBTestModule>
    {
    }
}
