using System.ComponentModel.DataAnnotations;
using SmartSoftware.Auditing;
using SmartSoftware.Validation;

namespace SmartSoftware.Identity;

public class IdentityUserCreateDto : IdentityUserCreateOrUpdateDtoBase
{
    [DisableAuditing]
    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
    public string Password { get; set; }
}
