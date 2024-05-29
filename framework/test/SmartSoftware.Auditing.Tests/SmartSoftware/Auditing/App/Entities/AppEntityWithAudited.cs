using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Auditing.App.Entities;

[Audited]
public class AppEntityWithAudited : AggregateRoot<Guid>
{
    protected AppEntityWithAudited()
    {

    }

    public AppEntityWithAudited(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}
