using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Auditing.App.Entities;

public class AppEntityWithPropertyHasAudited : AggregateRoot<Guid>
{
    protected AppEntityWithPropertyHasAudited()
    {

    }

    public AppEntityWithPropertyHasAudited(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    [Audited]
    public string Name { get; set; }
}
