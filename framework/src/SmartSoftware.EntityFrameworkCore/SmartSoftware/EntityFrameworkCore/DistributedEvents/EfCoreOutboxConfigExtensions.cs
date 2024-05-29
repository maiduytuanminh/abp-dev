﻿using SmartSoftware.Data;
using SmartSoftware.EventBus.Distributed;

namespace SmartSoftware.EntityFrameworkCore.DistributedEvents;

public static class EfCoreOutboxConfigExtensions
{
    public static void UseDbContext<TDbContext>(this OutboxConfig outboxConfig)
        where TDbContext : IHasEventOutbox
    {
        outboxConfig.ImplementationType = typeof(IDbContextEventOutbox<TDbContext>);
        outboxConfig.DatabaseName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
    }
}