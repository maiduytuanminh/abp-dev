using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartSoftware.Dapr;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.DistributedLocking.Dapr;

[Dependency(ReplaceServices = true)]
public class DaprSmartSoftwareDistributedLock : ISmartSoftwareDistributedLock, ITransientDependency
{
    protected ISmartSoftwareDaprClientFactory DaprClientFactory { get; }
    protected SmartSoftwareDistributedLockDaprOptions DistributedLockDaprOptions { get; }
    protected IDistributedLockKeyNormalizer DistributedLockKeyNormalizer { get; }

    public DaprSmartSoftwareDistributedLock(
        ISmartSoftwareDaprClientFactory daprClientFactory,
        IOptions<SmartSoftwareDistributedLockDaprOptions> distributedLockDaprOptions,
        IDistributedLockKeyNormalizer distributedLockKeyNormalizer)
    {
        DaprClientFactory = daprClientFactory;
        DistributedLockKeyNormalizer = distributedLockKeyNormalizer;
        DistributedLockDaprOptions = distributedLockDaprOptions.Value;
    }

    public async Task<ISmartSoftwareDistributedLockHandle?> TryAcquireAsync(
        string name,
        TimeSpan timeout = default,
        CancellationToken cancellationToken = default)
    {
        name = DistributedLockKeyNormalizer.NormalizeKey(name);

        var daprClient = await DaprClientFactory.CreateAsync();
        var lockResponse = await daprClient.Lock(
            DistributedLockDaprOptions.StoreName,
            name,
            DistributedLockDaprOptions.Owner ?? Guid.NewGuid().ToString(),
            (int)DistributedLockDaprOptions.DefaultExpirationTimeout.TotalSeconds,
            cancellationToken);

        if (lockResponse == null || !lockResponse.Success)
        {
            return null;
        }

        return new DaprSmartSoftwareDistributedLockHandle(lockResponse);
    }
}
