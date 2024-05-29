using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Application.Dtos;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Blogs;

namespace SmartSoftware.CmsKit.Admin.Blogs;

[Serializable]
public class CreateBlogDto : ExtensibleObject
{
    [Required]
    [DynamicMaxLength(typeof(BlogConsts), nameof(BlogConsts.MaxNameLength))]
    public string Name { get; set; }

    [Required]
    [DynamicMaxLength(typeof(BlogConsts), nameof(BlogConsts.MaxSlugLength))]
    public string Slug { get; set; }
}
