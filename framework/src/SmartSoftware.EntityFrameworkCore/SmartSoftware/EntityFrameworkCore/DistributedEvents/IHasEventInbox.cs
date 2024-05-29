using Microsoft.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public interface IHasEventInbox : IEfCoreDbContext
{
    DbSet<IncomingEventRecord> IncomingEvents { get; set; }
}
