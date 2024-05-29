using System;
using Confluent.Kafka;

namespace SmartSoftware.Kafka;

public interface IProducerPool : IDisposable
{
    IProducer<string, byte[]> Get(string? connectionName = null);
}
