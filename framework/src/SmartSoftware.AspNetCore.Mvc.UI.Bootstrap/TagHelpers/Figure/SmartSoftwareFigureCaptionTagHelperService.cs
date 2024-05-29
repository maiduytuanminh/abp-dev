using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure;

public class SmartSoftwareFigureCaptionTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareFigureCaptionTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "figcaption";
        output.Attributes.AddClass("figure-caption");
    }
}
