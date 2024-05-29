using System;
using Confluent.Kafka;

namespace SmartSoftware.Kafka;

public interface IConsumerPool : IDisposable
{
    IConsumer<string, byte[]> Get(string groupId, string? connectionName = null);
}
