using SmartSoftware.Data;
using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.MongoDB.DistributedEvents;

public static class MongoDbOutboxConfigExtensions
{
    public static void UseMongoDbContext<TMongoDbContext>(this OutboxConfig outboxConfig)
        where TMongoDbContext : IHasEventOutbox
    {
        outboxConfig.ImplementationType = typeof(IMongoDbContextEventOutbox<TMongoDbContext>);
        outboxConfig.DatabaseName = ConnectionStringNameAttribute.GetConnStringName<TMongoDbContext>();
    }
}
