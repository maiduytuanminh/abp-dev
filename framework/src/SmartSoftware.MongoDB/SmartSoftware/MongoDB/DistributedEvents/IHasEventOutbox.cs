using MongoDB.Driver;

namespace SmartSoftware.MongoDB.DistributedEvents;

public interface IHasEventOutbox : ISmartSoftwareMongoDbContext
{
    IMongoCollection<OutgoingEventRecord> OutgoingEvents { get; }
}
