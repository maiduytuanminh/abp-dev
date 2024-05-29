using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[HtmlTargetElement("ss-radio")]
public class SmartSoftwareRadioInputTagHelper : SmartSoftwareTagHelper<SmartSoftwareRadioInputTagHelper, SmartSoftwareRadioInputTagHelperService>
{
    public ModelExpression AspFor { get; set; } = default!;

    public string? Label { get; set; }

    public bool? Inline { get; set; }

    public bool? Disabled { get; set; }

    public IEnumerable<SelectListItem>? AspItems { get; set; }

    public SmartSoftwareRadioInputTagHelper(SmartSoftwareRadioInputTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
