using SmartSoftware.Application.Dtos;

namespace SmartSoftware.Identity;

public class GetIdentityUsersInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
