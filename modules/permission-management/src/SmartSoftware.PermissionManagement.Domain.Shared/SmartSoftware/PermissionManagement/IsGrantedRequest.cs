using System;

namespace SmartSoftware.PermissionManagement;

public class IsGrantedRequest
{
    public Guid UserId { get; set; }

    public string[] PermissionNames { get; set; }
}
