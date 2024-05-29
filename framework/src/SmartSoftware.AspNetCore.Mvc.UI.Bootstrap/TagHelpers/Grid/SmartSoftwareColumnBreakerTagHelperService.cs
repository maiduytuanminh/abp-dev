using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

public class SmartSoftwareColumnBreakerTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareColumnBreakerTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("w-100");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
