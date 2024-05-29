using SmartSoftware.Application.Dtos;

namespace SmartSoftware.Identity;

public class GetIdentityRolesInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
