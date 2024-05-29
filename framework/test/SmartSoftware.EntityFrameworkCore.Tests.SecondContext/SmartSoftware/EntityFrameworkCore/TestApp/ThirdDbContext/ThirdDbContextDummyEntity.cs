using System;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.EntityFrameworkCore.TestApp.ThirdDbContext;

public class ThirdDbContextDummyEntity : AggregateRoot<Guid>
{
    public string Value { get; set; }
}
