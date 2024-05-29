using System;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.Modeling;

namespace SmartSoftware.BackgroundJobs.EntityFrameworkCore;

public static class BackgroundJobsDbContextModelCreatingExtensions
{
    public static void ConfigureBackgroundJobs(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        if (builder.IsTenantOnlyDatabase())
        {
            return;
        }

        builder.Entity<BackgroundJobRecord>(b =>
        {
            b.ToTable(SmartSoftwareBackgroundJobsDbProperties.DbTablePrefix + "BackgroundJobs", SmartSoftwareBackgroundJobsDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.JobName).IsRequired().HasMaxLength(BackgroundJobRecordConsts.MaxJobNameLength);
            b.Property(x => x.JobArgs).IsRequired().HasMaxLength(BackgroundJobRecordConsts.MaxJobArgsLength);
            b.Property(x => x.TryCount).HasDefaultValue(0);
            b.Property(x => x.NextTryTime);
            b.Property(x => x.LastTryTime);
            b.Property(x => x.IsAbandoned).HasDefaultValue(false);
            b.Property(x => x.Priority).HasDefaultValue(BackgroundJobPriority.Normal).HasSentinel(BackgroundJobPriority.Normal);

            b.HasIndex(x => new { x.IsAbandoned, x.NextTryTime });

            b.ApplyObjectExtensionMappings();
        });

        builder.TryConfigureObjectExtensions<BackgroundJobsDbContext>();
    }
}
