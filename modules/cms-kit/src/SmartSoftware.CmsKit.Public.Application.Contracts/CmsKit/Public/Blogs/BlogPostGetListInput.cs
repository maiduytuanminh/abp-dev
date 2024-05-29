using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.CmsKit.Public.Blogs;

public class BlogPostGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? AuthorId { get; set; } 
    
    public Guid? TagId { get; set; }
}