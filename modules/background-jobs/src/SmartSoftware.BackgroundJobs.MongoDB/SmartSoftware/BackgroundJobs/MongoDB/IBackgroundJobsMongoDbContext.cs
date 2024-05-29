using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BackgroundJobs.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareBackgroundJobsDbProperties.ConnectionStringName)]
public interface IBackgroundJobsMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<BackgroundJobRecord> BackgroundJobs { get; }
}
