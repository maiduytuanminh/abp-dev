using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSoftware.EventBus.Distributed;

public interface IEventInbox
{
    Task EnqueueAsync(IncomingEventInfo incomingEvent);

    Task<List<IncomingEventInfo>> GetWaitingEventsAsync(int maxCount, CancellationToken cancellationToken = default);

    Task MarkAsProcessedAsync(Guid id);

    Task<bool> ExistsByMessageIdAsync(string messageId);

    Task DeleteOldEventsAsync();
}
