using JetBrains.Annotations;
using SmartSoftware.Data;

namespace SmartSoftware.MongoDB.DistributedEvents;

public static class EventOutboxMongoModelBuilderExtensions
{
    public static void ConfigureEventOutbox([NotNull] this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<OutgoingEventRecord>(b =>
        {
            b.CollectionName = SmartSoftwareCommonDbProperties.DbTablePrefix + "EventOutbox";
        });
    }
}
