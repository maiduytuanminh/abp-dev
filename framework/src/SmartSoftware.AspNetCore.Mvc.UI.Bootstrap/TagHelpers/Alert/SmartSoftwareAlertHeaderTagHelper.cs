using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

[HtmlTargetElement("h1", ParentTag = "ss-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h2", ParentTag = "ss-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h3", ParentTag = "ss-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h4", ParentTag = "ss-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h5", ParentTag = "ss-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h6", ParentTag = "ss-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareAlertHeaderTagHelper : SmartSoftwareTagHelper<SmartSoftwareAlertHeaderTagHelper, SmartSoftwareAlertHeaderTagHelperService>
{
    public SmartSoftwareAlertHeaderTagHelper(SmartSoftwareAlertHeaderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
