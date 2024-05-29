namespace SmartSoftware.RabbitMQ;

public class SmartSoftwareRabbitMqOptions
{
    public RabbitMqConnections Connections { get; }

    public SmartSoftwareRabbitMqOptions()
    {
        Connections = new RabbitMqConnections();
    }
}
