using System.Threading.Tasks;
using SmartSoftware.Threading;

namespace SmartSoftware.BackgroundJobs.RabbitMQ;

public interface IJobQueueManager : IRunnable
{
    Task<IJobQueue<TArgs>> GetAsync<TArgs>();
}
