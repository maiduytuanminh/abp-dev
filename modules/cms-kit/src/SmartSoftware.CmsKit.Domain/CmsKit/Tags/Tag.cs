using JetBrains.Annotations;
using System;
using SmartSoftware;
using SmartSoftware.Domain.Entities.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.CmsKit.Tags;

public class Tag : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    [NotNull]
    public virtual string EntityType { get; protected set; }

    [NotNull]
    public virtual string Name { get; protected set; }

    protected Tag()
    {
    }

    public Tag(
        Guid id,
        [NotNull] string entityType,
        [NotNull] string name,
        Guid? tenantId = null) : base(id)
    {
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType), TagConsts.MaxEntityTypeLength);
        Name = Check.NotNullOrEmpty(name, nameof(name), TagConsts.MaxNameLength);
        TenantId = tenantId;
    }

    public virtual void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrEmpty(name, nameof(name), TagConsts.MaxNameLength);
    }

    public virtual void SetEntityType(string entityType)
    {
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType), TagConsts.MaxEntityTypeLength);
    }
}
