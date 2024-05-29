using SmartSoftware.Collections;

namespace SmartSoftware.EventBus.Distributed;

public class SmartSoftwareDistributedEventBusOptions
{
    public ITypeList<IEventHandler> Handlers { get; }

    public OutboxConfigDictionary Outboxes { get; }

    public InboxConfigDictionary Inboxes { get; }
    public SmartSoftwareDistributedEventBusOptions()
    {
        Handlers = new TypeList<IEventHandler>();
        Outboxes = new OutboxConfigDictionary();
        Inboxes = new InboxConfigDictionary();
    }
}
