using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote;

[HtmlTargetElement("footer", ParentTag = "blockquote")]
public class SmartSoftwareBlockquoteFooterTagHelper : SmartSoftwareTagHelper<SmartSoftwareBlockquoteFooterTagHelper, SmartSoftwareBlockquoteFooterTagHelperService>
{
    public SmartSoftwareBlockquoteFooterTagHelper(SmartSoftwareBlockquoteFooterTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
