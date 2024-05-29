using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("img", Attributes = "ss-card-image", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("ss-image", Attributes = "ss-card-image", TagStructure = TagStructure.WithoutEndTag)]
public class SmartSoftwareCardImageTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardImageTagHelper, SmartSoftwareCardImageTagHelperService>
{
    [HtmlAttributeName("ss-card-image")]
    public SmartSoftwareCardImagePosition Position { get; set; } = SmartSoftwareCardImagePosition.Top;

    public SmartSoftwareCardImageTagHelper(SmartSoftwareCardImageTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
