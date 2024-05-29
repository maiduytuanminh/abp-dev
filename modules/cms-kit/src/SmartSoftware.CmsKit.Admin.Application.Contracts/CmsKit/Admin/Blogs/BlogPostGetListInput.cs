using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Admin.Blogs;

public class BlogPostGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    public Guid? BlogId { get; set; }

    public Guid? AuthorId { get; set; }
    
    public Guid? TagId { get; set; }

    public BlogPostStatus? Status { get; set; }
}