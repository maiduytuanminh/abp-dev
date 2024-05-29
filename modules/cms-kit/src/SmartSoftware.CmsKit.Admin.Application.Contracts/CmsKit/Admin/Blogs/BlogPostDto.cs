using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Auditing;
using SmartSoftware.Domain.Entities;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[Serializable]
public class BlogPostDto : ExtensibleEntityDto<Guid>, IHasCreationTime, IHasModificationTime, IHasConcurrencyStamp
{
    public Guid BlogId { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public Guid? CoverImageMediaId { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public string ConcurrencyStamp { get; set; }

    public BlogPostStatus Status { get; set; }
}
