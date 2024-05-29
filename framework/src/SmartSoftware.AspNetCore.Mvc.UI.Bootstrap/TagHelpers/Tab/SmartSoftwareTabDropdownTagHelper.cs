using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

[HtmlTargetElement("ss-tab-dropdown")]
public class SmartSoftwareTabDropdownTagHelper : SmartSoftwareTagHelper<SmartSoftwareTabDropdownTagHelper, SmartSoftwareTabDropdownTagHelperService>
{
    public string? Name { get; set; }

    public string Title { get; set; } = default!;

    public SmartSoftwareTabDropdownTagHelper(SmartSoftwareTabDropdownTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
