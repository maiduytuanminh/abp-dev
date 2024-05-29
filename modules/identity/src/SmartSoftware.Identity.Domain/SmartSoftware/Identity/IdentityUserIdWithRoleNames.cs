using System;
using System.Collections.Generic;

namespace SmartSoftware.Identity;

public class IdentityUserIdWithRoleNames
{
    public Guid Id { get; set; }

    public string[] RoleNames { get; set; }
}