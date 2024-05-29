using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.MongoDB.DistributedEvents;

public interface IMongoDbContextEventOutbox<TDbContext> : IEventOutbox
    where TDbContext : IHasEventOutbox
{

}
