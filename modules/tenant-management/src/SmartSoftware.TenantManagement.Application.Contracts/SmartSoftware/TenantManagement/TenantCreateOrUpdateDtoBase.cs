using System.ComponentModel.DataAnnotations;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;

namespace SmartSoftware.TenantManagement;

public abstract class TenantCreateOrUpdateDtoBase : ExtensibleObject
{
    [Required]
    [DynamicStringLength(typeof(TenantConsts), nameof(TenantConsts.MaxNameLength))]
    [Display(Name = "TenantName")]
    public string Name { get; set; }

    public TenantCreateOrUpdateDtoBase() : base(false)
    {

    }
}
