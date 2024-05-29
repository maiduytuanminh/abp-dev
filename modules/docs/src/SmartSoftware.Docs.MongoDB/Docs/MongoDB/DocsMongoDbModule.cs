using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;
using SmartSoftware.Docs.Projects;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs.MongoDB
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(SmartSoftwareMongoDbModule)
    )]
    public class DocsMongoDbModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<DocsMongoDbContext>(options =>
            {
                options.AddRepository<Project, MongoProjectRepository>();
                options.AddRepository<Document, MongoDocumentRepository>();
            });
        }
    }
}
