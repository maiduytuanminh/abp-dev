using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.MongoDB.TestApp.FifthContext;

public class FifthDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
