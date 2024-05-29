using System;

namespace SmartSoftware.Identity;

public class IdentityRoleWithUserCount
{
    public IdentityRole Role { get; set; }

    public long UserCount { get; set; }
    
    public IdentityRoleWithUserCount(IdentityRole role, long userCount)
    {
        Role = role;
        UserCount = userCount;
    }
}
