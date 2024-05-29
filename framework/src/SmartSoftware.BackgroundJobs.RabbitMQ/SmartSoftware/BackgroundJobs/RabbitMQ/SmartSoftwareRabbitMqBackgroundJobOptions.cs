using System;
using System.Collections.Generic;

namespace SmartSoftware.BackgroundJobs.RabbitMQ;

public class SmartSoftwareRabbitMqBackgroundJobOptions
{
    /// <summary>
    /// Key: Job Args Type
    /// </summary>
    public Dictionary<Type, JobQueueConfiguration> JobQueues { get; }

    /// <summary>
    /// Default value: "SmartSoftwareBackgroundJobs.".
    /// </summary>
    public string DefaultQueueNamePrefix { get; set; }

    /// <summary>
    /// Default value: "SmartSoftwareBackgroundJobsDelayed."
    /// </summary>
    public string DefaultDelayedQueueNamePrefix { get; set; }
    
    public ushort? PrefetchCount { get; set; }

    public SmartSoftwareRabbitMqBackgroundJobOptions()
    {
        JobQueues = new Dictionary<Type, JobQueueConfiguration>();
        DefaultQueueNamePrefix = "SmartSoftwareBackgroundJobs.";
        DefaultDelayedQueueNamePrefix = "SmartSoftwareBackgroundJobsDelayed.";
    }
}
