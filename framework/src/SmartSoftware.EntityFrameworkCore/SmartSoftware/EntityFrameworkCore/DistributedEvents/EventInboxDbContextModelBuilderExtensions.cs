using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore.Modeling;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public static class EventInboxDbContextModelBuilderExtensions
{
    public static void ConfigureEventInbox([NotNull] this ModelBuilder builder)
    {
        builder.Entity<IncomingEventRecord>(b =>
        {
            b.ToTable(SmartSoftwareCommonDbProperties.DbTablePrefix + "EventInbox", SmartSoftwareCommonDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.EventName).IsRequired().HasMaxLength(IncomingEventRecord.MaxEventNameLength);
            b.Property(x => x.EventData).IsRequired();

            b.HasIndex(x => new { x.Processed, x.CreationTime });
            b.HasIndex(x => x.MessageId);
        });
    }
}
