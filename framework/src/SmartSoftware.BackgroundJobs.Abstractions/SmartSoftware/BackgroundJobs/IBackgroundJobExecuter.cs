using System.Threading.Tasks;

namespace SmartSoftware.BackgroundJobs;

public interface IBackgroundJobExecuter
{
    Task ExecuteAsync(JobExecutionContext context);
}
