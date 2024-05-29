using System;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus;

namespace SmartSoftware.Data;

[Serializable]
[EventName("ss.data.apply_database_migrations")]
public class ApplyDatabaseMigrationsEto : EtoBase
{
    public Guid? TenantId { get; set; }

    public string DatabaseName { get; set; } = default!;
}
