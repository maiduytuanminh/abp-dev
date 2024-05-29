using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(SmartSoftwareEntityFrameworkCoreModule))]
    public class DocsEntityFrameworkCoreModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSmartSoftwareDbContext<DocsDbContext>(options =>
            {
                options.AddRepository<Project, EfCoreProjectRepository>();
                options.AddRepository<Document, EFCoreDocumentRepository>();
            });
        }
    }
}
