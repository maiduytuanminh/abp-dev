﻿using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.MongoDB.TestApp.SecondContext;

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
