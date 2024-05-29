using System;
using RabbitMQ.Client;

namespace SmartSoftware.RabbitMQ;

public interface IConnectionPool : IDisposable
{
    IConnection Get(string? connectionName = null);
}
