using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AsyncKeyedLock;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.DistributedLocking;

public class LocalSmartSoftwareDistributedLock : ISmartSoftwareDistributedLock, ISingletonDependency
{
    private readonly AsyncKeyedLocker<string> _localSyncObjects = new(o =>
    {
        o.PoolSize = 20;
        o.PoolInitialFill = 1;
    });
    protected IDistributedLockKeyNormalizer DistributedLockKeyNormalizer { get; }

    public LocalSmartSoftwareDistributedLock(IDistributedLockKeyNormalizer distributedLockKeyNormalizer)
    {
        DistributedLockKeyNormalizer = distributedLockKeyNormalizer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task<ISmartSoftwareDistributedLockHandle?> TryAcquireAsync(
        string name,
        TimeSpan timeout = default,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        var key = DistributedLockKeyNormalizer.NormalizeKey(name);

        var timeoutReleaser = await _localSyncObjects.LockAsync(key, timeout, cancellationToken);
        if (!timeoutReleaser.EnteredSemaphore)
        {
            timeoutReleaser.Dispose();
            return null;
        }
        return new LocalSmartSoftwareDistributedLockHandle(timeoutReleaser);
    }
}
