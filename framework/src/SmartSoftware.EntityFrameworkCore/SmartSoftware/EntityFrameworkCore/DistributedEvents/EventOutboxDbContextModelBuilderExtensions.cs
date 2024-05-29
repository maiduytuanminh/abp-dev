using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore.Modeling;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public static class EventOutboxDbContextModelBuilderExtensions
{
    public static void ConfigureEventOutbox([NotNull] this ModelBuilder builder)
    {
        builder.Entity<OutgoingEventRecord>(b =>
        {
            b.ToTable(SmartSoftwareCommonDbProperties.DbTablePrefix + "EventOutbox", SmartSoftwareCommonDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.EventName).IsRequired().HasMaxLength(OutgoingEventRecord.MaxEventNameLength);
            b.Property(x => x.EventData).IsRequired();

            b.HasIndex(x => x.CreationTime);
        });
    }
}
