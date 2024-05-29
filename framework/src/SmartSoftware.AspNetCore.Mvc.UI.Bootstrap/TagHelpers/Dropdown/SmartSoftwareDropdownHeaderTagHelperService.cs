using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class SmartSoftwareDropdownHeaderTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareDropdownHeaderTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h6";
        output.Attributes.AddClass("dropdown-header");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
