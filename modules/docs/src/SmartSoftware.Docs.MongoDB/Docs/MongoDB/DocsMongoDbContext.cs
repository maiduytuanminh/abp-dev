using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.Docs.Projects;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs.MongoDB
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(SmartSoftwareDocsDbProperties.ConnectionStringName)]
    public class DocsMongoDbContext : SmartSoftwareMongoDbContext, IDocsMongoDbContext
    {
        public IMongoCollection<Project> Projects => Collection<Project>();
        public IMongoCollection<Document> Documents => Collection<Document>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDocs();
        }
    }
}
