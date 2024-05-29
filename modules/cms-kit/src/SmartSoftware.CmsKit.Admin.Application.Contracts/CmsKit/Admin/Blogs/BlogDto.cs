using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[Serializable]
public class BlogDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; }

    public string Slug { get; set; }
    public string ConcurrencyStamp { get; set; }
}
