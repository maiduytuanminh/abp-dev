using System;
using SmartSoftware.EventBus;

namespace SmartSoftware.Data;

[Serializable]
[EventName("ss.data.applied_database_migrations")]
public class AppliedDatabaseMigrationsEto
{
    public string DatabaseName { get; set; } = default!;
    public Guid? TenantId { get; set; }
}