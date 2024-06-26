﻿using System;
using System.Collections.Generic;
using System.Linq;
using SmartSoftware.Auditing;
using SmartSoftware.Data;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Guids;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.AuditLogging;

[DisableAuditing]
public class EntityChange : Entity<Guid>, IMultiTenant, IHasExtraProperties
{
    public virtual Guid AuditLogId { get; protected set; }

    public virtual Guid? TenantId { get; protected set; }

    public virtual DateTime ChangeTime { get; protected set; }

    public virtual EntityChangeType ChangeType { get; protected set; }

    public virtual Guid? EntityTenantId { get; protected set; }

    public virtual string EntityId { get; protected set; }

    public virtual string EntityTypeFullName { get; protected set; }

    public virtual ICollection<EntityPropertyChange> PropertyChanges { get; protected set; }

    public virtual ExtraPropertyDictionary ExtraProperties { get; protected set; }

    protected EntityChange()
    {
        ExtraProperties = new ExtraPropertyDictionary();
    }

    public EntityChange(
        IGuidGenerator guidGenerator,
        Guid auditLogId,
        EntityChangeInfo entityChangeInfo,
        Guid? tenantId = null)
    {
        Id = guidGenerator.Create();
        AuditLogId = auditLogId;
        TenantId = tenantId;
        ChangeTime = entityChangeInfo.ChangeTime;
        ChangeType = entityChangeInfo.ChangeType;
        EntityTenantId = entityChangeInfo.EntityTenantId;
        EntityId = entityChangeInfo.EntityId.Truncate(EntityChangeConsts.MaxEntityTypeFullNameLength);
        EntityTypeFullName = entityChangeInfo.EntityTypeFullName.TruncateFromBeginning(EntityChangeConsts.MaxEntityTypeFullNameLength);

        PropertyChanges = entityChangeInfo
                              .PropertyChanges?
                              .Select(p => new EntityPropertyChange(guidGenerator, Id, p, tenantId))
                              .ToList()
                          ?? new List<EntityPropertyChange>();

        ExtraProperties = new ExtraPropertyDictionary();
        if (entityChangeInfo.ExtraProperties != null)
        {
            foreach (var pair in entityChangeInfo.ExtraProperties)
            {
                ExtraProperties.Add(pair.Key, pair.Value);
            }
        }
    }
}
