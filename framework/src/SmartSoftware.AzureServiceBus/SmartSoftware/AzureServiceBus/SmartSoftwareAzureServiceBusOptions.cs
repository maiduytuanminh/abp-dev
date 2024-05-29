namespace SmartSoftware.AzureServiceBus;

public class SmartSoftwareAzureServiceBusOptions
{
    public AzureServiceBusConnections Connections { get; }

    public SmartSoftwareAzureServiceBusOptions()
    {
        Connections = new AzureServiceBusConnections();
    }
}
