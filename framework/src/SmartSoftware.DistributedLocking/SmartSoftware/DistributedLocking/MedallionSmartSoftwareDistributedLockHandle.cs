using System.Threading.Tasks;
using Medallion.Threading;

namespace SmartSoftware.DistributedLocking;

public class MedallionSmartSoftwareDistributedLockHandle : ISmartSoftwareDistributedLockHandle
{
    public IDistributedSynchronizationHandle Handle { get; }

    public MedallionSmartSoftwareDistributedLockHandle(IDistributedSynchronizationHandle handle)
    {
        Handle = handle;
    }

    public ValueTask DisposeAsync()
    {
        return Handle.DisposeAsync();
    }
}
