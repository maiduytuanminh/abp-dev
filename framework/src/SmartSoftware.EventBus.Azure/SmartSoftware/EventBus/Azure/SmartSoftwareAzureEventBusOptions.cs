namespace SmartSoftware.EventBus.Azure;

public class SmartSoftwareAzureEventBusOptions
{
    public string? ConnectionName { get; set; }

    public string SubscriberName { get; set; } = default!;

    public string TopicName { get; set; } = default!;

    public bool IsServiceBusDisabled { get; set; }
}
