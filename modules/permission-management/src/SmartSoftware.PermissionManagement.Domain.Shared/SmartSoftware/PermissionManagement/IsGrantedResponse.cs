using System;
using System.Collections.Generic;

namespace SmartSoftware.PermissionManagement;

public class IsGrantedResponse
{
    public Guid UserId { get; set; }

    public Dictionary<string, bool> Permissions { get; set; }
}
