using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Identity;

public class IdentityUserUpdateRolesDto
{
    [Required]
    public string[] RoleNames { get; set; }
}
