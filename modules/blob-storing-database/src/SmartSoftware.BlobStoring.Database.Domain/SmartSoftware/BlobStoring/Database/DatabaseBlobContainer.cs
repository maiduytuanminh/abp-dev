using System;
using JetBrains.Annotations;
using SmartSoftware.Domain.Entities;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.BlobStoring.Database;

public class DatabaseBlobContainer : AggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual string Name { get; protected set; }

    protected DatabaseBlobContainer()
    {
    }

    public DatabaseBlobContainer(Guid id, [NotNull] string name, Guid? tenantId = null)
        : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DatabaseContainerConsts.MaxNameLength);
        TenantId = tenantId;
    }
}
