using SmartSoftware.Data;

namespace SmartSoftware.MongoDB.DistributedEvents;

public static class EventInboxMongoModelBuilderExtensions
{
    public static void ConfigureEventInbox(this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<IncomingEventRecord>(b =>
        {
            b.CollectionName = SmartSoftwareCommonDbProperties.DbTablePrefix + "EventInbox";
        });
    }
}
