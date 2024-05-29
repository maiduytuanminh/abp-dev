using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public interface IDbContextEventInbox<TDbContext> : IEventInbox
    where TDbContext : IHasEventInbox
{

}
