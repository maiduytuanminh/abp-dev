using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("a", Attributes = "ss-card-link")]
public class SmartSoftwareCardLinkTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardLinkTagHelper, SmartSoftwareCardLinkTagHelperService>
{
    public SmartSoftwareCardLinkTagHelper(SmartSoftwareCardLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
