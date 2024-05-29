using System.Collections.Generic;

namespace SmartSoftware.PermissionManagement;

public class GetPermissionListResultDto
{
    public string EntityDisplayName { get; set; }

    public List<PermissionGroupDto> Groups { get; set; }
}
