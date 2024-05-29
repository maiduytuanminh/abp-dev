using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

[HtmlTargetElement("a", Attributes = "ss-alert-link", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareAlertLinkTagHelper : SmartSoftwareTagHelper<SmartSoftwareAlertLinkTagHelper, SmartSoftwareAlertLinkTagHelperService>
{
    public SmartSoftwareAlertLinkTagHelper(SmartSoftwareAlertLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
