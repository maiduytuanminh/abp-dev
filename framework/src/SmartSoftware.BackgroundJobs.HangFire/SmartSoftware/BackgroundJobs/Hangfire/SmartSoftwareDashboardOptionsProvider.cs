using System.Linq;
using System.Threading;
using Hangfire;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.BackgroundJobs.Hangfire;

public class SmartSoftwareDashboardOptionsProvider : ITransientDependency
{
    protected SmartSoftwareBackgroundJobOptions SmartSoftwareBackgroundJobOptions { get; }

    public SmartSoftwareDashboardOptionsProvider(IOptions<SmartSoftwareBackgroundJobOptions> ssBackgroundJobOptions)
    {
        SmartSoftwareBackgroundJobOptions = ssBackgroundJobOptions.Value;
    }

    public virtual DashboardOptions Get()
    {
        return new DashboardOptions
        {
            DisplayNameFunc = (_, job) =>
            {
                var jobName = job.ToString();
                if (job.Args.Count == 3 && job.Args.Last() is CancellationToken)
                {
                    jobName = SmartSoftwareBackgroundJobOptions.GetJob(job.Args[1].GetType()).JobName;
                }

                return jobName;
            }
        };
    }
}
