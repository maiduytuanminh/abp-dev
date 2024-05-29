using System;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus;

namespace SmartSoftware.MultiTenancy;

[Serializable]
[EventName("ss.multi_tenancy.tenant.created")]
public class TenantCreatedEto : EtoBase
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}
