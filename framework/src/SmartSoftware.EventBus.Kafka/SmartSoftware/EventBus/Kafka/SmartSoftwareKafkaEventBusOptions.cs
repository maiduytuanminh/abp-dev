namespace SmartSoftware.EventBus.Kafka;

public class SmartSoftwareKafkaEventBusOptions
{

    public string? ConnectionName { get; set; }

    public string TopicName { get; set; } = default!;

    public string GroupId { get; set; } = default!;
}
