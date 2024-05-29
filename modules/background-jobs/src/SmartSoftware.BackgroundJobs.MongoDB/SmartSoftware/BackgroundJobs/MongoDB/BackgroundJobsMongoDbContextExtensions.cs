using SmartSoftware.MongoDB;

namespace SmartSoftware.BackgroundJobs.MongoDB;

public static class BackgroundJobsMongoDbContextExtensions
{
    public static void ConfigureBackgroundJobs(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<BackgroundJobRecord>(b =>
        {
            b.CollectionName = SmartSoftwareBackgroundJobsDbProperties.DbTablePrefix + "BackgroundJobs";
        });
    }
}
