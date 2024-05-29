using SmartSoftware.Docs.MongoDB;
using Xunit;

namespace SmartSoftware.Docs.Document
{
    [Collection(MongoTestCollection.Name)]
    public class DocumentRepository_Tests : DocumentRepository_Tests<DocsMongoDBTestModule>
    {

    }
}