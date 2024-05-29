using System;
using SmartSoftware.Domain.Entities.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Identity;

/// <summary>
/// Represents membership of a User to an OU.
/// </summary>
public class IdentityUserOrganizationUnit : CreationAuditedEntity, IMultiTenant
{
    /// <summary>
    /// TenantId of this entity.
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// Id of the User.
    /// </summary>
    public virtual Guid UserId { get; protected set; }

    /// <summary>
    /// Id of the related <see cref="OrganizationUnit"/>.
    /// </summary>
    public virtual Guid OrganizationUnitId { get; protected set; }

    protected IdentityUserOrganizationUnit()
    {

    }

    public IdentityUserOrganizationUnit(Guid userId, Guid organizationUnitId, Guid? tenantId = null)
    {
        UserId = userId;
        OrganizationUnitId = organizationUnitId;
        TenantId = tenantId;
    }

    public override object[] GetKeys()
    {
        return new object[] { UserId, OrganizationUnitId };
    }
}
