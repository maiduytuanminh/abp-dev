using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace SmartSoftware.AzureServiceBus;

public interface IAzureServiceBusMessageConsumer
{
    void OnMessageReceived(Func<ServiceBusReceivedMessage, Task> callback);
}
