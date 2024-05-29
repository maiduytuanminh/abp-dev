﻿using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SmartSoftware.BackgroundJobs.Hangfire;

public class HangfireJobExecutionAdapter<TArgs>
{
    protected SmartSoftwareBackgroundJobOptions Options { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IBackgroundJobExecuter JobExecuter { get; }

    public HangfireJobExecutionAdapter(
        IOptions<SmartSoftwareBackgroundJobOptions> options,
        IBackgroundJobExecuter jobExecuter,
        IServiceScopeFactory serviceScopeFactory)
    {
        JobExecuter = jobExecuter;
        ServiceScopeFactory = serviceScopeFactory;
        Options = options.Value;
    }

    [Queue("{0}")]
    public async Task ExecuteAsync(string queue, TArgs args, CancellationToken cancellationToken = default)
    {
        if (!Options.IsJobExecutionEnabled)
        {
            throw new SmartSoftwareException(
                "Background job execution is disabled. " +
                "This method should not be called! " +
                "If you want to enable the background job execution, " +
                $"set {nameof(SmartSoftwareBackgroundJobOptions)}.{nameof(SmartSoftwareBackgroundJobOptions.IsJobExecutionEnabled)} to true! " +
                "If you've intentionally disabled job execution and this seems a bug, please report it."
            );
        }

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var jobType = Options.GetJob(typeof(TArgs)).JobType;
            var context = new JobExecutionContext(scope.ServiceProvider, jobType, args!, cancellationToken: cancellationToken);
            await JobExecuter.ExecuteAsync(context);
        }
    }
}
