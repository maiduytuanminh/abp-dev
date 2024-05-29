using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote;

[HtmlTargetElement("p", ParentTag = "blockquote")]
public class SmartSoftwareBlockquoteParagraphTagHelper : SmartSoftwareTagHelper<SmartSoftwareBlockquoteParagraphTagHelper, SmartSoftwareBlockquoteParagraphTagHelperService>
{
    public SmartSoftwareBlockquoteParagraphTagHelper(SmartSoftwareBlockquoteParagraphTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
