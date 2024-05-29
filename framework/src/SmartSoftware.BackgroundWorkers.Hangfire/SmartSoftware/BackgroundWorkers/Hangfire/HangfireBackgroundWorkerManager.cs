using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DynamicProxy;
using SmartSoftware.Hangfire;
using SmartSoftware.Threading;

namespace SmartSoftware.BackgroundWorkers.Hangfire;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IBackgroundWorkerManager), typeof(HangfireBackgroundWorkerManager))]
public class HangfireBackgroundWorkerManager : BackgroundWorkerManager, ISingletonDependency
{
    protected SmartSoftwareHangfireBackgroundJobServer BackgroundJobServer { get; set; } = default!;
    protected IServiceProvider ServiceProvider { get; }

    public HangfireBackgroundWorkerManager(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public void Initialize()
    {
        BackgroundJobServer = ServiceProvider.GetRequiredService<SmartSoftwareHangfireBackgroundJobServer>();
    }

    public async override Task AddAsync(IBackgroundWorker worker, CancellationToken cancellationToken = default)
    {
        switch (worker)
        {
            case IHangfireBackgroundWorker hangfireBackgroundWorker:
            {
                var unProxyWorker = ProxyHelper.UnProxy(hangfireBackgroundWorker);
                if (hangfireBackgroundWorker.RecurringJobId.IsNullOrWhiteSpace())
                {
                    RecurringJob.AddOrUpdate(
                        () => ((IHangfireBackgroundWorker)unProxyWorker).DoWorkAsync(cancellationToken),
                        hangfireBackgroundWorker.CronExpression, hangfireBackgroundWorker.TimeZone,
                        hangfireBackgroundWorker.Queue);
                }
                else
                {
                    RecurringJob.AddOrUpdate(hangfireBackgroundWorker.RecurringJobId,
                        () => ((IHangfireBackgroundWorker)unProxyWorker).DoWorkAsync(cancellationToken),
                        hangfireBackgroundWorker.CronExpression, hangfireBackgroundWorker.TimeZone,
                        hangfireBackgroundWorker.Queue);
                }

                break;
            }
            case AsyncPeriodicBackgroundWorkerBase or PeriodicBackgroundWorkerBase:
            {
                var timer = worker.GetType()
                    .GetProperty("Timer", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(worker);

                var period = worker is AsyncPeriodicBackgroundWorkerBase ? ((SmartSoftwareAsyncTimer?)timer)?.Period : ((SmartSoftwareTimer?)timer)?.Period;

                if (period == null)
                {
                    return;
                }

                var adapterType = typeof(HangfirePeriodicBackgroundWorkerAdapter<>).MakeGenericType(ProxyHelper.GetUnProxiedType(worker));
                var workerAdapter = (Activator.CreateInstance(adapterType) as IHangfireBackgroundWorker)!;

                RecurringJob.AddOrUpdate(() => workerAdapter.DoWorkAsync(cancellationToken), GetCron(period.Value), workerAdapter.TimeZone, workerAdapter.Queue);

                break;
            }
            default:
                await base.AddAsync(worker, cancellationToken);
                break;
        }
    }

    protected virtual string GetCron(int period)
    {
        var time = TimeSpan.FromMilliseconds(period);
        string cron;

        if (time.TotalSeconds <= 59)
        {
            cron = $"*/{time.TotalSeconds} * * * * *";
        }
        else if (time.TotalMinutes <= 59)
        {
            cron = $"*/{time.TotalMinutes} * * * *";
        }
        else if (time.TotalHours <= 23)
        {
            cron = $"0 */{time.TotalHours} * * *";
        }
        else
        {
            throw new SmartSoftwareException(
                $"Cannot convert period: {period} to cron expression, use HangfireBackgroundWorkerBase to define worker");
        }

        return cron;
    }
}