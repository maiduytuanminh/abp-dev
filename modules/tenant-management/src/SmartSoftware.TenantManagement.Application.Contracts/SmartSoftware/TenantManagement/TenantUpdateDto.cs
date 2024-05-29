using SmartSoftware.Domain.Entities;

namespace SmartSoftware.TenantManagement;

public class TenantUpdateDto : TenantCreateOrUpdateDtoBase, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
