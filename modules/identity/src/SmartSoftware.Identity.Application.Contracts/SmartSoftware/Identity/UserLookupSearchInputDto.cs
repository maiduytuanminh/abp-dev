using SmartSoftware.Application.Dtos;

namespace SmartSoftware.Identity;

public class UserLookupSearchInputDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
