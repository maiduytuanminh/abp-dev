using Microsoft.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public interface IHasEventOutbox : IEfCoreDbContext
{
    DbSet<OutgoingEventRecord> OutgoingEvents { get; set; }
}
