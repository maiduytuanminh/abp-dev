using System;
using SmartSoftware.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Identity;

[Serializable]
public class OrganizationUnitEto : IMultiTenant, IHasEntityVersion
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Code { get; set; }

    public string DisplayName { get; set; }

    public int EntityVersion { get; set; }
}
