using System;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus;

namespace SmartSoftware.MultiTenancy;

[Serializable]
[EventName("ss.multi_tenancy.tenant.connection_string.updated")]
public class TenantConnectionStringUpdatedEto : EtoBase
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string ConnectionStringName { get; set; } = default!;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }
}
