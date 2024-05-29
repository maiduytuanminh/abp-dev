using System;

namespace SmartSoftware.RabbitMQ;

public interface IChannelPool : IDisposable
{
    IChannelAccessor Acquire(string? channelName = null, string? connectionName = null);
}
