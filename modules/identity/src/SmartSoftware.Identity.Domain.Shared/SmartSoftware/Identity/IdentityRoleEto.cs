using System;
using SmartSoftware.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Identity;

[Serializable]
public class IdentityRoleEto : IMultiTenant, IHasEntityVersion
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; }

    public bool IsDefault { get; set; }

    public bool IsStatic { get; set; }

    public bool IsPublic { get; set; }

    public int EntityVersion { get; set; }
}
