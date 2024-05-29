using System;
using SmartSoftware.Domain.Entities;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.MongoDB.TestApp.FifthContext;

public class FifthDbContextMultiTenantDummyEntity : AggregateRoot<Guid>, IMultiTenant
{
    public string Value { get; set; }

    public Guid? TenantId { get; set; }
}
