using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Reflection;
using SmartSoftware.Threading;
using SmartSoftware.Uow;

namespace SmartSoftware.EventBus.Local;

/// <summary>
/// Implements EventBus as Singleton pattern.
/// </summary>
[ExposeServices(typeof(ILocalEventBus), typeof(LocalEventBus))]
public class LocalEventBus : EventBusBase, ILocalEventBus, ISingletonDependency
{
    /// <summary>
    /// Reference to the Logger.
    /// </summary>
    public ILogger<LocalEventBus> Logger { get; set; }

    protected SmartSoftwareLocalEventBusOptions Options { get; }

    protected ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }

    public LocalEventBus(
        IOptions<SmartSoftwareLocalEventBusOptions> options,
        IServiceScopeFactory serviceScopeFactory,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        IEventHandlerInvoker eventHandlerInvoker)
        : base(serviceScopeFactory, currentTenant, unitOfWorkManager, eventHandlerInvoker)
    {
        Options = options.Value;
        Logger = NullLogger<LocalEventBus>.Instance;

        HandlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
        SubscribeHandlers(Options.Handlers);
    }

    /// <inheritdoc/>
    public virtual IDisposable Subscribe<TEvent>(ILocalEventHandler<TEvent> handler) where TEvent : class
    {
        return Subscribe(typeof(TEvent), handler);
    }

    /// <inheritdoc/>
    public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
    {
        GetOrCreateHandlerFactories(eventType)
            .Locking(factories =>
                {
                    if (!factory.IsInFactories(factories))
                    {
                        factories.Add(factory);
                    }
                }
            );

        return new EventHandlerFactoryUnregistrar(this, eventType, factory);
    }

    /// <inheritdoc/>
    public override void Unsubscribe<TEvent>(Func<TEvent, Task> action)
    {
        Check.NotNull(action, nameof(action));

        GetOrCreateHandlerFactories(typeof(TEvent))
            .Locking(factories =>
            {
                factories.RemoveAll(
                    factory =>
                    {
                        var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
                        if (singleInstanceFactory == null)
                        {
                            return false;
                        }

                        var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEvent>;
                        if (actionHandler == null)
                        {
                            return false;
                        }

                        return actionHandler.Action == action;
                    });
            });
    }

    /// <inheritdoc/>
    public override void Unsubscribe(Type eventType, IEventHandler handler)
    {
        GetOrCreateHandlerFactories(eventType)
            .Locking(factories =>
            {
                factories.RemoveAll(
                    factory =>
                        factory is SingleInstanceHandlerFactory &&
                        ((factory as SingleInstanceHandlerFactory)!).HandlerInstance == handler
                );
            });
    }

    /// <inheritdoc/>
    public override void Unsubscribe(Type eventType, IEventHandlerFactory factory)
    {
        GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
    }

    /// <inheritdoc/>
    public override void UnsubscribeAll(Type eventType)
    {
        GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
    }

    protected override async Task PublishToEventBusAsync(Type eventType, object eventData)
    {
        await PublishAsync(new LocalEventMessage(Guid.NewGuid(), eventData, eventType));
    }

    protected override void AddToUnitOfWork(IUnitOfWork unitOfWork, UnitOfWorkEventRecord eventRecord)
    {
        unitOfWork.AddOrReplaceLocalEvent(eventRecord);
    }

    public virtual async Task PublishAsync(LocalEventMessage localEventMessage)
    {
        await TriggerHandlersAsync(localEventMessage.EventType, localEventMessage.EventData);
    }

    protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
    {
        var handlerFactoryList = new List<Tuple<IEventHandlerFactory, Type, int>>();
        foreach (var handlerFactory in HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
        {
            foreach (var factory in handlerFactory.Value)
            {
                handlerFactoryList.Add(new Tuple<IEventHandlerFactory, Type, int>(
                    factory,
                    handlerFactory.Key,
                    ReflectionHelper.GetAttributesOfMemberOrDeclaringType<LocalEventHandlerOrderAttribute>(factory.GetHandler().EventHandler.GetType()).FirstOrDefault()?.Order ?? 0));
            }
        }

        return handlerFactoryList.OrderBy(x => x.Item3).Select(x => new EventTypeWithEventHandlerFactories(x.Item2, new List<IEventHandlerFactory> {x.Item1})).ToArray();
    }

    private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
    {
        return HandlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());
    }

    private static bool ShouldTriggerEventForHandler(Type targetEventType, Type handlerEventType)
    {
        //Should trigger same type
        if (handlerEventType == targetEventType)
        {
            return true;
        }

        //Should trigger for inherited types
        if (handlerEventType.IsAssignableFrom(targetEventType))
        {
            return true;
        }

        return false;
    }

    // Internal for unit testing
    internal Func<Type, object, Task>? OnEventHandleInvoking { get; set; }

    // Internal for unit testing
    protected async override Task InvokeEventHandlerAsync(IEventHandler eventHandler, object eventData, Type eventType)
    {
        if (OnEventHandleInvoking != null && eventType != typeof(DistributedEventSent) && eventType != typeof(DistributedEventReceived))
        {
            await OnEventHandleInvoking(eventType, eventData);
        }

        await base.InvokeEventHandlerAsync(eventHandler, eventData, eventType);
    }

    // Internal for unit testing
    internal Func<Type, object, Task>? OnPublishing { get; set; }

    // For unit testing
    public async override Task PublishAsync(
        Type eventType,
        object eventData,
        bool onUnitOfWorkComplete = true)
    {
        if (onUnitOfWorkComplete && UnitOfWorkManager.Current != null)
        {
            AddToUnitOfWork(
                UnitOfWorkManager.Current,
                new UnitOfWorkEventRecord(eventType, eventData, EventOrderGenerator.GetNext())
            );
            return;
        }

        // For unit testing
        if (OnPublishing != null && eventType != typeof(DistributedEventSent) && eventType != typeof(DistributedEventReceived))
        {
            await OnPublishing(eventType, eventData);
        }

        await PublishToEventBusAsync(eventType, eventData);
    }
}