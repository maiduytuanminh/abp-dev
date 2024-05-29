using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.Validation.Localization;
using SmartSoftware.CmsKit.Localization;
using SmartSoftware.CmsKit.Tags;

namespace SmartSoftware.CmsKit.Admin.Tags;

[Serializable]
public class EntityTagSetDto : IValidatableObject
{
    public string EntityId { get; set; }

    public string EntityType { get; set; }

    [Required]
    public List<string> Tags { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var l = validationContext.GetRequiredService<IStringLocalizer<SmartSoftwareValidationResource>>();

        foreach (var tag in Tags)
        {
            if (tag.Length > TagConsts.MaxNameLength)
            {
                yield return new ValidationResult(
                    l[
                        "ThisFieldMustBeAStringWithAMaximumLengthOf{0}",
                        TagConsts.MaxNameLength
                    ],
                    new[] { nameof(Tags) }
                );
            }
        }
    }
}
