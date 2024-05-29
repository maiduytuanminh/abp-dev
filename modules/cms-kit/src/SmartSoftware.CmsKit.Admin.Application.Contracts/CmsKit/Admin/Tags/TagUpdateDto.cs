using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Domain.Entities;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Tags;

[Serializable]
public class TagUpdateDto : ExtensibleObject, IHasConcurrencyStamp
{
    [Required]
    [DynamicMaxLength(typeof(TagConsts), nameof(TagConsts.MaxNameLength))]
    public string Name { get; set; }

    public string ConcurrencyStamp { get; set; }
}
