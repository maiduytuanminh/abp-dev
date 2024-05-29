using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public interface IDbContextEventOutbox<TDbContext> : IEventOutbox
    where TDbContext : IHasEventOutbox
{

}
