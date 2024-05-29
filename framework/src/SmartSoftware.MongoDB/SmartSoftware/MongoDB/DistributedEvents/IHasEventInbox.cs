using MongoDB.Driver;

namespace SmartSoftware.MongoDB.DistributedEvents;

public interface IHasEventInbox : ISmartSoftwareMongoDbContext
{
    IMongoCollection<IncomingEventRecord> IncomingEvents { get; }
}
