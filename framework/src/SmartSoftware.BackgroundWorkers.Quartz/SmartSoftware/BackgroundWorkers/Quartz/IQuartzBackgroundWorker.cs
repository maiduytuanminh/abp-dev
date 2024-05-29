using System;
using System.Threading.Tasks;
using Quartz;

namespace SmartSoftware.BackgroundWorkers.Quartz;

public interface IQuartzBackgroundWorker : IBackgroundWorker, IJob
{
    ITrigger Trigger { get; set; }

    IJobDetail JobDetail { get; set; }

    bool AutoRegister { get; set; }

    Func<IScheduler, Task>? ScheduleJob { get; set; }
}
