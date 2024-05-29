using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Auditing;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[Serializable]
public class BlogPostListDto : ExtensibleEntityDto<Guid>, IHasCreationTime, IHasModificationTime
{
    public Guid BlogId { get; set; }

    public string BlogName { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public Guid? CoverImageMediaId { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? LastModificationTime { get; set; }
    
    public BlogPostStatus? Status { get; set; }
}
