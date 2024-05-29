using SmartSoftware.Data;
using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public static class EfCoreInboxConfigExtensions
{
    public static void UseDbContext<TDbContext>(this InboxConfig outboxConfig)
        where TDbContext : IHasEventInbox
    {
        outboxConfig.ImplementationType = typeof(IDbContextEventInbox<TDbContext>);
        outboxConfig.DatabaseName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
    }
}
