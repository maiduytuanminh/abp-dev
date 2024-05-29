using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BackgroundJobs.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareBackgroundJobsDbProperties.ConnectionStringName)]
public class BackgroundJobsMongoDbContext : SmartSoftwareMongoDbContext, IBackgroundJobsMongoDbContext
{
    public IMongoCollection<BackgroundJobRecord> BackgroundJobs { get; set; }

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureBackgroundJobs();
    }
}
