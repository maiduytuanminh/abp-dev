﻿using System.Threading.Tasks;
using Rebus.Handlers;

namespace SmartSoftware.EventBus.Rebus;

public class RebusDistributedEventHandlerAdapter<TEventData> : IHandleMessages<TEventData> , IRebusDistributedEventHandlerAdapter
{
    protected RebusDistributedEventBus RebusDistributedEventBus { get; }

    public RebusDistributedEventHandlerAdapter(RebusDistributedEventBus rebusDistributedEventBus)
    {
        RebusDistributedEventBus = rebusDistributedEventBus;
    }

    public async Task Handle(TEventData message)
    {
        await RebusDistributedEventBus.ProcessEventAsync(message!.GetType(), message);
    }
}
