namespace SmartSoftware.EventBus.Dapr;

public class SmartSoftwareDaprEventData
{
    public string PubSubName { get; set; }

    public string Topic { get; set; }

    public string MessageId { get; set; }

    public string JsonData { get; set; }

    public string? CorrelationId { get; set; }

    public SmartSoftwareDaprEventData(string pubSubName, string topic, string messageId, string jsonData, string? correlationId)
    {
        PubSubName = pubSubName;
        Topic = topic;
        MessageId = messageId;
        JsonData = jsonData;
        CorrelationId = correlationId;
    }
}
