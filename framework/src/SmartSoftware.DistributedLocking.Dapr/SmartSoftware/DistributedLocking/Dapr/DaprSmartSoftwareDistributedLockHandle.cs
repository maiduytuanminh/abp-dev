using System.Threading.Tasks;
using Dapr.Client;

namespace SmartSoftware.DistributedLocking.Dapr;

public class DaprSmartSoftwareDistributedLockHandle : ISmartSoftwareDistributedLockHandle
{
    protected TryLockResponse LockResponse { get; }

    public DaprSmartSoftwareDistributedLockHandle(TryLockResponse lockResponse)
    {
        LockResponse = lockResponse;
    }

    public async ValueTask DisposeAsync()
    {
        await LockResponse.DisposeAsync();
    }
}
