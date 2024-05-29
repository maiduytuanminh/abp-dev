using System;
using System.Threading.Tasks;

namespace SmartSoftware.EventBus;

public interface IEventHandlerInvoker
{
    Task InvokeAsync(IEventHandler eventHandler, object eventData, Type eventType);
}
