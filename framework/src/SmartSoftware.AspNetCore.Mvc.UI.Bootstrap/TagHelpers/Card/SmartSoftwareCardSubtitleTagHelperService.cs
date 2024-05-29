using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardSubtitleTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareCardSubtitleTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = TagHelper.Heading.ToHtmlTag();
        output.Attributes.AddClass("card-subtitle");
    }
}
