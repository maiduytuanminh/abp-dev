using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.TestApp.Domain;

public class Category : AggregateRoot<Guid>, ISoftDelete
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
