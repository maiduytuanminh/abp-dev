using System;
using Medallion.Threading;

namespace SmartSoftware.DistributedLocking;

public static class SmartSoftwareDistributedLockHandleExtensions
{
    public static IDistributedSynchronizationHandle ToDistributedSynchronizationHandle(
        this ISmartSoftwareDistributedLockHandle handle)
    {
        return handle.As<MedallionSmartSoftwareDistributedLockHandle>().Handle;
    }
}
