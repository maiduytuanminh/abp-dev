using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.EntityFrameworkCore
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(SmartSoftwareDocsDbProperties.ConnectionStringName)]
    public class DocsDbContext: SmartSoftwareDbContext<DocsDbContext>, IDocsDbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentContributor> DocumentContributors { get; set; }

        public DocsDbContext(DbContextOptions<DocsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDocs();
        }
    }
}
