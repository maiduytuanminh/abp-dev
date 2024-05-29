using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.MongoDB.DistributedEvents;

public interface IMongoDbContextEventInbox<TDbContext> : IEventInbox
    where TDbContext : IHasEventInbox
{

}
