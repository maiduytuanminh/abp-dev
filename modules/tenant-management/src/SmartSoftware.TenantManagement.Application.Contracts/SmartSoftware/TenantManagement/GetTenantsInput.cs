using SmartSoftware.Application.Dtos;

namespace SmartSoftware.TenantManagement;

public class GetTenantsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
