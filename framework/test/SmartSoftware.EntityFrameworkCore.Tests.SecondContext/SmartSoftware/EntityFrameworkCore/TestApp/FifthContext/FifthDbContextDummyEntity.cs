using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FifthContext;

public class FifthDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
