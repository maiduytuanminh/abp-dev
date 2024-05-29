using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("ss-card", Attributes = "text-color")]
[HtmlTargetElement("ss-card-header", Attributes = "text-color")]
[HtmlTargetElement("ss-card-body", Attributes = "text-color")]
[HtmlTargetElement("ss-card-footer", Attributes = "text-color")]
public class SmartSoftwareCardTextColorTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardTextColorTagHelper, SmartSoftwareCardTextColorTagHelperService>
{
    public SmartSoftwareCardTextColorType TextColor { get; set; } = SmartSoftwareCardTextColorType.Default;

    public SmartSoftwareCardTextColorTagHelper(SmartSoftwareCardTextColorTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
