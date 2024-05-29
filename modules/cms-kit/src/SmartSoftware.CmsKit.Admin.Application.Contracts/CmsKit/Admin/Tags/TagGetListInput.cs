using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Admin.Tags;

[Serializable]
public class TagGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
