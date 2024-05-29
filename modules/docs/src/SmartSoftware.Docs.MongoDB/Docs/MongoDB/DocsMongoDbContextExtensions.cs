using SmartSoftware;
using SmartSoftware.MongoDB;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs.MongoDB
{
    public static class DocsMongoDbContextExtensions
    {
        public static void ConfigureDocs(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Project>(b =>
            {
                b.CollectionName = SmartSoftwareDocsDbProperties.DbTablePrefix + "Projects";
            });

            builder.Entity<Document>(b =>
            {
                b.CollectionName = SmartSoftwareDocsDbProperties.DbTablePrefix + "DocumentS";
            });
        }
    }
}

