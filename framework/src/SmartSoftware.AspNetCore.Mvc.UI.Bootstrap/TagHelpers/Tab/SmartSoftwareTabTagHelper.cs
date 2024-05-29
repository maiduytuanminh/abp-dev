using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

[HtmlTargetElement("ss-tab")]
public class SmartSoftwareTabTagHelper : SmartSoftwareTagHelper<SmartSoftwareTabTagHelper, SmartSoftwareTabTagHelperService>
{
    public string? Name { get; set; }

    public string Title { get; set; } = default!;

    public bool? Active { get; set; }

    public string? ParentDropdownName { get; set; }

    public SmartSoftwareTabTagHelper(SmartSoftwareTabTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
