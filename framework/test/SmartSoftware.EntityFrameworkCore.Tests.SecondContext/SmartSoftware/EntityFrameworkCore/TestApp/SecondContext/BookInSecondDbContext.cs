using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.TestApp.SecondContext;

public class BookInSecondDbContext : AggregateRoot<Guid>
{
    public string Name { get; set; }

    public BookInSecondDbContext()
    {

    }

    public BookInSecondDbContext(Guid id, string name)
        : base(id)
    {
        Name = name;
    }
}
