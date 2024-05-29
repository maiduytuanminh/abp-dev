using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.DistributedEvents;

namespace DistDemoApp
{
    [ConnectionStringName("Default")]
    public class TodoMongoDbContext : SmartSoftwareMongoDbContext, IHasEventOutbox, IHasEventInbox
    {
        public IMongoCollection<TodoItem> TodoItems => Collection<TodoItem>();
        public IMongoCollection<TodoSummary> TodoSummaries => Collection<TodoSummary>();

        public IMongoCollection<OutgoingEventRecord> OutgoingEvents
        {
            get => Collection<OutgoingEventRecord>();
            set {}
        }
        public IMongoCollection<IncomingEventRecord> IncomingEvents
        {
            get => Collection<IncomingEventRecord>();
            set {}
        }
    }

}
