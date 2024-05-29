using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Public.Blogs;

[Serializable]
public class BlogPostFilteredPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
