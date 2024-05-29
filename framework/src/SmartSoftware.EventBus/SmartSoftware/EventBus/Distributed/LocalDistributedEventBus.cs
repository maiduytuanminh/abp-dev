using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Collections;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus.Local;

namespace SmartSoftware.EventBus.Distributed;

[Dependency(TryRegister = true)]
[ExposeServices(typeof(IDistributedEventBus), typeof(LocalDistributedEventBus))]
public class LocalDistributedEventBus : IDistributedEventBus, ISingletonDependency
{
    private readonly ILocalEventBus _localEventBus;

    protected IServiceScopeFactory ServiceScopeFactory { get; }

    protected SmartSoftwareDistributedEventBusOptions SmartSoftwareDistributedEventBusOptions { get; }

    public LocalDistributedEventBus(
        ILocalEventBus localEventBus,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<SmartSoftwareDistributedEventBusOptions> distributedEventBusOptions)
    {
        _localEventBus = localEventBus;
        ServiceScopeFactory = serviceScopeFactory;
        SmartSoftwareDistributedEventBusOptions = distributedEventBusOptions.Value;
        Subscribe(distributedEventBusOptions.Value.Handlers);

        // For unit testing
        if (localEventBus is LocalEventBus eventBus)
        {
            eventBus.OnEventHandleInvoking = async (eventType, eventData) =>
            {
                await localEventBus.PublishAsync(new DistributedEventReceived()
                {
                    Source = DistributedEventSource.Direct,
                    EventName = EventNameAttribute.GetNameOrDefault(eventType),
                    EventData = eventData
                }, onUnitOfWorkComplete: false);
            };

            eventBus.OnPublishing = async (eventType, eventData) =>
            {
                await localEventBus.PublishAsync(new DistributedEventSent()
                {
                    Source = DistributedEventSource.Direct,
                    EventName = EventNameAttribute.GetNameOrDefault(eventType),
                    EventData = eventData
                }, onUnitOfWorkComplete: false);
            };
        }
    }

    public virtual void Subscribe(ITypeList<IEventHandler> handlers)
    {
        foreach (var handler in handlers)
        {
            var interfaces = handler.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    Subscribe(genericArgs[0], new IocEventHandlerFactory(ServiceScopeFactory, handler));
                }
            }
        }
    }

    /// <inheritdoc/>
    public virtual IDisposable Subscribe<TEvent>(IDistributedEventHandler<TEvent> handler) where TEvent : class
    {
        return Subscribe(typeof(TEvent), handler);
    }

    public IDisposable Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
    {
        return _localEventBus.Subscribe(action);
    }

    public IDisposable Subscribe<TEvent>(ILocalEventHandler<TEvent> handler) where TEvent : class
    {
        return _localEventBus.Subscribe(handler);
    }

    public IDisposable Subscribe<TEvent, THandler>() where TEvent : class where THandler : IEventHandler, new()
    {
        return _localEventBus.Subscribe<TEvent, THandler>();
    }

    public IDisposable Subscribe(Type eventType, IEventHandler handler)
    {
        return _localEventBus.Subscribe(eventType, handler);
    }

    public IDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
    {
        return _localEventBus.Subscribe<TEvent>(factory);
    }

    public IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
    {
        return _localEventBus.Subscribe(eventType, factory);
    }

    public void Unsubscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
    {
        _localEventBus.Unsubscribe(action);
    }

    public void Unsubscribe<TEvent>(ILocalEventHandler<TEvent> handler) where TEvent : class
    {
        _localEventBus.Unsubscribe(handler);
    }

    public void Unsubscribe(Type eventType, IEventHandler handler)
    {
        _localEventBus.Unsubscribe(eventType, handler);
    }

    public void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
    {
        _localEventBus.Unsubscribe<TEvent>(factory);
    }

    public void Unsubscribe(Type eventType, IEventHandlerFactory factory)
    {
        _localEventBus.Unsubscribe(eventType, factory);
    }

    public void UnsubscribeAll<TEvent>() where TEvent : class
    {
        _localEventBus.UnsubscribeAll<TEvent>();
    }

    public void UnsubscribeAll(Type eventType)
    {
        _localEventBus.UnsubscribeAll(eventType);
    }

    public Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true)
        where TEvent : class
    {
        return _localEventBus.PublishAsync(eventData, onUnitOfWorkComplete);
    }

    public Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true)
    {
        return _localEventBus.PublishAsync(eventType, eventData, onUnitOfWorkComplete);
    }

    public Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true, bool useOutbox = true) where TEvent : class
    {
        return _localEventBus.PublishAsync(eventData, onUnitOfWorkComplete);
    }

    public Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true, bool useOutbox = true)
    {
        return _localEventBus.PublishAsync(eventType, eventData, onUnitOfWorkComplete);
    }
}