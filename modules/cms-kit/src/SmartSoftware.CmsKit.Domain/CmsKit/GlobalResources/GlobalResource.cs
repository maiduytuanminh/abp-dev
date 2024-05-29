using System;
using JetBrains.Annotations;
using SmartSoftware;
using SmartSoftware.Domain.Entities.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.CmsKit.GlobalResources;

public class GlobalResource : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual string Name { get; private set; }
    
    public virtual string Value { get; private set; }

    public virtual Guid? TenantId { get; protected set; }
    
    
    protected GlobalResource()
    {
    }

    internal GlobalResource(
        Guid id,
        [NotNull] string name,
        [CanBeNull] string value,
        Guid? tenantId = null) : base(id)
    {
        Name = Check.NotNullOrEmpty(name, nameof(name), GlobalResourceConsts.MaxNameLength);
        Value = Check.Length(value, nameof(value), GlobalResourceConsts.MaxValueLength);
        
        TenantId = tenantId;
    }

    public virtual void SetValue(string value)
    {
        Check.Length(value, nameof(value), GlobalResourceConsts.MaxValueLength);

        Value = value;
    }
}