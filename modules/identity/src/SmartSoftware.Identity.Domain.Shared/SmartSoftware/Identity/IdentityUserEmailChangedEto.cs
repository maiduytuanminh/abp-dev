using System;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Identity;

[Serializable]
public class IdentityUserEmailChangedEto : IMultiTenant 
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Email { get; set; }

    public string OldEmail { get; set; }
}