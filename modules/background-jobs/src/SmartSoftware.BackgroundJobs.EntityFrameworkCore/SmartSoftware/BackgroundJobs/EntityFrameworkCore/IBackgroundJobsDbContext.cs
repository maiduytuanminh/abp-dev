using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BackgroundJobs.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareBackgroundJobsDbProperties.ConnectionStringName)]
public interface IBackgroundJobsDbContext : IEfCoreDbContext
{
    DbSet<BackgroundJobRecord> BackgroundJobs { get; }
}
