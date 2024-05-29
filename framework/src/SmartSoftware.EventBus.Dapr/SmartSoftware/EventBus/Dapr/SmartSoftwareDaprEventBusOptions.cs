namespace SmartSoftware.EventBus.Dapr;

public class SmartSoftwareDaprEventBusOptions
{
    public string PubSubName { get; set; }

    public SmartSoftwareDaprEventBusOptions()
    {
        PubSubName = "pubsub";
    }
}
