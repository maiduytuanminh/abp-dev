﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.EventBus.Local;
using SmartSoftware.Guids;
using SmartSoftware.Kafka;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;
using SmartSoftware.Timing;
using SmartSoftware.Tracing;
using SmartSoftware.Uow;

namespace SmartSoftware.EventBus.Kafka;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IDistributedEventBus), typeof(KafkaDistributedEventBus))]
public class KafkaDistributedEventBus : DistributedEventBusBase, ISingletonDependency
{
    protected SmartSoftwareKafkaEventBusOptions SmartSoftwareKafkaEventBusOptions { get; }
    protected IKafkaMessageConsumerFactory MessageConsumerFactory { get; }
    protected IKafkaSerializer Serializer { get; }
    protected IProducerPool ProducerPool { get; }
    protected ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }
    protected ConcurrentDictionary<string, Type> EventTypes { get; }
    protected IKafkaMessageConsumer Consumer { get; private set; } = default!;

    public KafkaDistributedEventBus(
        IServiceScopeFactory serviceScopeFactory,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        IOptions<SmartSoftwareKafkaEventBusOptions> ssKafkaEventBusOptions,
        IKafkaMessageConsumerFactory messageConsumerFactory,
        IOptions<SmartSoftwareDistributedEventBusOptions> ssDistributedEventBusOptions,
        IKafkaSerializer serializer,
        IProducerPool producerPool,
        IGuidGenerator guidGenerator,
        IClock clock,
        IEventHandlerInvoker eventHandlerInvoker,
        ILocalEventBus localEventBus,
        ICorrelationIdProvider correlationIdProvider)
        : base(
            serviceScopeFactory,
            currentTenant,
            unitOfWorkManager,
            ssDistributedEventBusOptions,
            guidGenerator,
            clock,
            eventHandlerInvoker,
            localEventBus,
            correlationIdProvider)
    {
        SmartSoftwareKafkaEventBusOptions = ssKafkaEventBusOptions.Value;
        MessageConsumerFactory = messageConsumerFactory;
        Serializer = serializer;
        ProducerPool = producerPool;

        HandlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
        EventTypes = new ConcurrentDictionary<string, Type>();
    }

    public void Initialize()
    {
        Consumer = MessageConsumerFactory.Create(
            SmartSoftwareKafkaEventBusOptions.TopicName,
            SmartSoftwareKafkaEventBusOptions.GroupId,
            SmartSoftwareKafkaEventBusOptions.ConnectionName);
        Consumer.OnMessageReceived(ProcessEventAsync);

        SubscribeHandlers(SmartSoftwareDistributedEventBusOptions.Handlers);
    }

    private async Task ProcessEventAsync(Message<string, byte[]> message)
    {
        var eventName = message.Key;
        var eventType = EventTypes.GetOrDefault(eventName);
        if (eventType == null)
        {
            return;
        }

        var messageId = message.GetMessageId();
        var eventData = Serializer.Deserialize(message.Value, eventType);
        var correlationId = message.GetCorrelationId();

        if (await AddToInboxAsync(messageId, eventName, eventType, eventData, correlationId))
        {
            return;
        }

        using (CorrelationIdProvider.Change(correlationId))
        {
            await TriggerHandlersDirectAsync(eventType, eventData);
        }
    }

    public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
    {
        var handlerFactories = GetOrCreateHandlerFactories(eventType);

        if (factory.IsInFactories(handlerFactories))
        {
            return NullDisposable.Instance;
        }

        handlerFactories.Add(factory);

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
                        factory is SingleInstanceHandlerFactory handlerFactory &&
                        handlerFactory.HandlerInstance == handler
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

    protected async override Task PublishToEventBusAsync(Type eventType, object eventData)
    {
        var headers = new Headers
        {
            { "messageId", System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N")) }
        };

        if (CorrelationIdProvider.Get() != null)
        {
            headers.Add(EventBusConsts.CorrelationIdHeaderName, System.Text.Encoding.UTF8.GetBytes(CorrelationIdProvider.Get()!));
        }

        await PublishAsync(
            SmartSoftwareKafkaEventBusOptions.TopicName,
            eventType,
            eventData,
            headers
        );
    }

    protected override void AddToUnitOfWork(IUnitOfWork unitOfWork, UnitOfWorkEventRecord eventRecord)
    {
        unitOfWork.AddOrReplaceDistributedEvent(eventRecord);
    }

    public async override Task PublishFromOutboxAsync(
        OutgoingEventInfo outgoingEvent,
        OutboxConfig outboxConfig)
    {
        using (CorrelationIdProvider.Change(outgoingEvent.GetCorrelationId()))
        {
            await TriggerDistributedEventSentAsync(new DistributedEventSent()
            {
                Source = DistributedEventSource.Outbox,
                EventName = outgoingEvent.EventName,
                EventData = outgoingEvent.EventData
            });
        }

        var headers = new Headers
        {
            { "messageId", System.Text.Encoding.UTF8.GetBytes(outgoingEvent.Id.ToString("N")) }
        };
        if (outgoingEvent.GetCorrelationId() != null)
        {
            headers.Add(EventBusConsts.CorrelationIdHeaderName, System.Text.Encoding.UTF8.GetBytes(outgoingEvent.GetCorrelationId()!));
        }

        await PublishAsync(
            SmartSoftwareKafkaEventBusOptions.TopicName,
            outgoingEvent.EventName,
            outgoingEvent.EventData,
            headers
        );
    }

    public async override Task PublishManyFromOutboxAsync(IEnumerable<OutgoingEventInfo> outgoingEvents, OutboxConfig outboxConfig)
    {
        var producer = ProducerPool.Get(SmartSoftwareKafkaEventBusOptions.ConnectionName);
        var outgoingEventArray = outgoingEvents.ToArray();

        foreach (var outgoingEvent in outgoingEventArray)
        {
            var messageId = outgoingEvent.Id.ToString("N");
            var headers = new Headers
            {
                { "messageId", System.Text.Encoding.UTF8.GetBytes(messageId)}
            };

            if (outgoingEvent.GetCorrelationId() != null)
            {
                headers.Add(EventBusConsts.CorrelationIdHeaderName, System.Text.Encoding.UTF8.GetBytes(outgoingEvent.GetCorrelationId()!));
            }

            using (CorrelationIdProvider.Change(outgoingEvent.GetCorrelationId()))
            {
                await TriggerDistributedEventSentAsync(new DistributedEventSent()
                {
                    Source = DistributedEventSource.Outbox,
                    EventName = outgoingEvent.EventName,
                    EventData = outgoingEvent.EventData
                });
            }

            producer.Produce(
                SmartSoftwareKafkaEventBusOptions.TopicName,
                new Message<string, byte[]>
                {
                    Key = outgoingEvent.EventName,
                    Value = outgoingEvent.EventData,
                    Headers = headers
                });
        }
    }

    public async override Task ProcessFromInboxAsync(
        IncomingEventInfo incomingEvent,
        InboxConfig inboxConfig)
    {
        var eventType = EventTypes.GetOrDefault(incomingEvent.EventName);
        if (eventType == null)
        {
            return;
        }

        var eventData = Serializer.Deserialize(incomingEvent.EventData, eventType);
        var exceptions = new List<Exception>();
        using (CorrelationIdProvider.Change(incomingEvent.GetCorrelationId()))
        {
            await TriggerHandlersFromInboxAsync(eventType, eventData, exceptions, inboxConfig);
        }
        if (exceptions.Any())
        {
            ThrowOriginalExceptions(eventType, exceptions);
        }
    }

    protected override byte[] Serialize(object eventData)
    {
        return Serializer.Serialize(eventData);
    }

    private Task PublishAsync(string topicName, Type eventType, object eventData, Headers headers)
    {
        var eventName = EventNameAttribute.GetNameOrDefault(eventType);
        var body = Serializer.Serialize(eventData);

        return PublishAsync(topicName, eventName, body, headers);
    }

    private Task<DeliveryResult<string, byte[]>> PublishAsync(
        string topicName,
        string eventName,
        byte[] body,
        Headers headers)
    {
        var producer = ProducerPool.Get(SmartSoftwareKafkaEventBusOptions.ConnectionName);

        return producer.ProduceAsync(
            topicName,
            new Message<string, byte[]>
            {
                Key = eventName,
                Value = body,
                Headers = headers
            });
    }

    protected override Task OnAddToOutboxAsync(string eventName, Type eventType, object eventData)
    {
        EventTypes.GetOrAdd(eventName, eventType);
        return base.OnAddToOutboxAsync(eventName, eventType, eventData);
    }

    private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
    {
        return HandlerFactories.GetOrAdd(
            eventType,
            type =>
            {
                var eventName = EventNameAttribute.GetNameOrDefault(type);
                EventTypes.GetOrAdd(eventName, eventType);
                return new List<IEventHandlerFactory>();
            }
        );
    }

    protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
    {
        var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

        foreach (var handlerFactory in HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key))
        )
        {
            handlerFactoryList.Add(
                new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
        }

        return handlerFactoryList.ToArray();
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
}
