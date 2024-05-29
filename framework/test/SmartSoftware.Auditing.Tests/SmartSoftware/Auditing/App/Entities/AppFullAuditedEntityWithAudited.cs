using System;
using SmartSoftware.Domain.Entities.Auditing;

namespace SmartSoftware.Auditing.App.Entities;

[Audited]
public class AppFullAuditedEntityWithAudited : FullAuditedAggregateRoot<Guid>
{
    protected AppFullAuditedEntityWithAudited()
    {
    }

    public AppFullAuditedEntityWithAudited(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}
