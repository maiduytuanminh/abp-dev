using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;

public class FourthDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
