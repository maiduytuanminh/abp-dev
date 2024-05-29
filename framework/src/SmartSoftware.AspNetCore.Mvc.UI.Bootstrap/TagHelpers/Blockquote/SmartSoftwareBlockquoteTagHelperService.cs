using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote;

public class SmartSoftwareBlockquoteTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareBlockquoteTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("blockquote");
        output.TagName = "blockquote";
    }
}
