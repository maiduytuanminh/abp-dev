using System.Threading;
using System.Threading.Tasks;

namespace SmartSoftware.EventBus.Distributed;

public interface IOutboxSender
{
    Task StartAsync(OutboxConfig outboxConfig, CancellationToken cancellationToken = default);

    Task StopAsync(CancellationToken cancellationToken = default);
}
