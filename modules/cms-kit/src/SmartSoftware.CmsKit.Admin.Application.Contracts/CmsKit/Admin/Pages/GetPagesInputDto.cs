using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Admin.Pages;

[Serializable]
public class GetPagesInputDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
