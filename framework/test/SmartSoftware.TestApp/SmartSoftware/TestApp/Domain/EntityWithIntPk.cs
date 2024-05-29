using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.TestApp.Domain;

public class EntityWithIntPk : AggregateRoot<int>
{
    public string Name { get; set; }

    public EntityWithIntPk()
    {

    }

    public EntityWithIntPk(string name)
    {
        Name = name;
    }
}
