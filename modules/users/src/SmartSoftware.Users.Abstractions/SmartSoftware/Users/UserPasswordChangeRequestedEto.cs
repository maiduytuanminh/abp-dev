using System;
using SmartSoftware.EventBus;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Users;

[Serializable]
[EventName("SmartSoftware.Users.UserPasswordChangeRequested")]
public class UserPasswordChangeRequestedEto : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
}
