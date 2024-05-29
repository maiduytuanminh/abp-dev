using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Admin.Menus;

[Serializable]
public class PageLookupInputDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
