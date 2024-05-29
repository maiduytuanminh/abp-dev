using System;

namespace SmartSoftware.EventBus;

public interface IEventNameProvider
{
    string GetName(Type eventType);
}
