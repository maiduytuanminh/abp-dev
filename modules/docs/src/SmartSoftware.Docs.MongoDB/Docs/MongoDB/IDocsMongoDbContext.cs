using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.MongoDB
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(SmartSoftwareDocsDbProperties.ConnectionStringName)]
    public interface IDocsMongoDbContext : ISmartSoftwareMongoDbContext
    {
        IMongoCollection<Project> Projects { get; }

        IMongoCollection<Document> Documents { get; }
    }
}
