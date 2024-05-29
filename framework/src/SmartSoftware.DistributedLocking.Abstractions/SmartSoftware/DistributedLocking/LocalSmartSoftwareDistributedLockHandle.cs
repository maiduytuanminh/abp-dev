using System;
using System.Threading.Tasks;

namespace SmartSoftware.DistributedLocking;

public class LocalSmartSoftwareDistributedLockHandle : ISmartSoftwareDistributedLockHandle
{
    private readonly IDisposable _disposable;

    public LocalSmartSoftwareDistributedLockHandle(IDisposable disposable)
    {
        _disposable = disposable;
    }

    public ValueTask DisposeAsync()
    {
        _disposable.Dispose();
        return default;
    }
}
