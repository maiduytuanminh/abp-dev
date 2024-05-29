using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ProgressBar;

public class SmartSoftwareProgressGroupTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareProgressGroupTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("progress");
        output.TagName = "div";
    }
}
