using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardTitleTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareCardTitleTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = TagHelper.Heading.ToHtmlTag();
        output.Attributes.AddClass("card-title");
    }
}
