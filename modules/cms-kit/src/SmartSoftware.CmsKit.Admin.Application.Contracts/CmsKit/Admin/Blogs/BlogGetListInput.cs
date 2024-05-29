using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[Serializable]
public class BlogGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
