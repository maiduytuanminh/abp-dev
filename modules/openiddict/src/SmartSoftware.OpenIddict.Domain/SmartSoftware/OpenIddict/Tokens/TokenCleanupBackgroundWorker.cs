using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.BackgroundWorkers;
using SmartSoftware.DistributedLocking;
using SmartSoftware.Threading;

namespace SmartSoftware.OpenIddict.Tokens;

public class TokenCleanupBackgroundWorker : AsyncPeriodicBackgroundWorkerBase
{
    protected ISmartSoftwareDistributedLock DistributedLock { get; }

    public TokenCleanupBackgroundWorker(
        SmartSoftwareAsyncTimer timer,
        IServiceScopeFactory serviceScopeFactory,
        IOptionsMonitor<TokenCleanupOptions> cleanupOptions,
        ISmartSoftwareDistributedLock distributedLock)
        : base(timer, serviceScopeFactory)
    {
        DistributedLock = distributedLock;
        timer.Period = cleanupOptions.CurrentValue.CleanupPeriod;
    }

    protected async override Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        await using (var handle = await DistributedLock.TryAcquireAsync(nameof(TokenCleanupBackgroundWorker)))
        {
            Logger.LogInformation($"Lock is acquired for {nameof(TokenCleanupBackgroundWorker)}");

            if (handle != null)
            {
                await workerContext
                    .ServiceProvider
                    .GetRequiredService<TokenCleanupService>()
                    .CleanAsync();

                Logger.LogInformation($"Lock is released for {nameof(TokenCleanupBackgroundWorker)}");
                return;
            }

            Logger.LogInformation($"Handle is null because of the locking for : {nameof(TokenCleanupBackgroundWorker)}");
        }
    }
}
