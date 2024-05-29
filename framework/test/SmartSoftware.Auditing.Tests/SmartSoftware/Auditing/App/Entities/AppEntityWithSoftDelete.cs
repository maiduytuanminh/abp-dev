using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Auditing.App.Entities;

public class AppEntityWithSoftDelete : AggregateRoot<Guid>, IHasDeletionTime
{
    public AppEntityWithSoftDelete(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletionTime { get; set; }
}
