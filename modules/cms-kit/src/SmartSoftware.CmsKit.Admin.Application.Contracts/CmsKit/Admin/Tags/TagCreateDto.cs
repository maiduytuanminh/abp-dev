using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Tags;

[Serializable]
public class TagCreateDto : ExtensibleObject
{
    [Required]
    [DynamicMaxLength(typeof(TagConsts), nameof(TagConsts.MaxEntityTypeLength))]
    public string EntityType { get; set; }

    [Required]
    [DynamicMaxLength(typeof(TagConsts), nameof(TagConsts.MaxNameLength))]
    public string Name { get; set; }
}
