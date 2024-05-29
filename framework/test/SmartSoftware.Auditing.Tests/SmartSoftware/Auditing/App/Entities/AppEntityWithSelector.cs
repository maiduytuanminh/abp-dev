﻿using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Auditing.App.Entities;

public class AppEntityWithSelector : AggregateRoot<Guid>
{
    public AppEntityWithSelector(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}
