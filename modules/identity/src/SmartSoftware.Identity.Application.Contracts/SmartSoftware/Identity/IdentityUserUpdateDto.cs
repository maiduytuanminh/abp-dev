using System.ComponentModel.DataAnnotations;
using SmartSoftware.Auditing;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Validation;

namespace SmartSoftware.Identity;

public class IdentityUserUpdateDto : IdentityUserCreateOrUpdateDtoBase, IHasConcurrencyStamp
{
    [DisableAuditing]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
    public string Password { get; set; }

    public string ConcurrencyStamp { get; set; }
}
