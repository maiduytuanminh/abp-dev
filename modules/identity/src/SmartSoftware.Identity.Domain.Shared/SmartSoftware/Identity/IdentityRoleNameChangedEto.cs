using System;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Identity;

[Serializable]
public class IdentityRoleNameChangedEto : IMultiTenant 
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; }

    public string OldName { get; set; }
}
