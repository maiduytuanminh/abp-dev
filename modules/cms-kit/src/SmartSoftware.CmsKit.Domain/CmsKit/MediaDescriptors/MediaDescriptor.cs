using System;
using SmartSoftware;
using SmartSoftware.Domain.Entities.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public class MediaDescriptor : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; protected set; }

    public string EntityType { get; protected set; }

    public string Name { get; protected set; }

    public string MimeType { get; protected set; }

    public long Size { get; protected set; }

    protected MediaDescriptor()
    {

    }

    internal MediaDescriptor(Guid id, string entityType, string name, string mimeType, long size, Guid? tenantId = null) : base(id)
    {
        TenantId = tenantId;

        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType), MediaDescriptorConsts.MaxEntityTypeLength);
        MimeType = Check.NotNullOrWhiteSpace(mimeType, nameof(name), MediaDescriptorConsts.MaxMimeTypeLength);
        Size = size;

        SetName(name);
    }

    public void SetName(string name)
    {
        if (!MediaDescriptorChecks.IsValidMediaFileName(name))
        {
            throw new InvalidMediaDescriptorNameException(name);
        }

        Name = name;
    }
}
