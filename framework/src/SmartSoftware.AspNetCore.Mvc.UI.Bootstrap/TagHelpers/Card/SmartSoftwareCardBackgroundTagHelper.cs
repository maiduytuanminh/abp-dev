using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("ss-card", Attributes = "background")]
[HtmlTargetElement("ss-card-header", Attributes = "background")]
[HtmlTargetElement("ss-card-body", Attributes = "background")]
[HtmlTargetElement("ss-card-footer", Attributes = "background")]
public class SmartSoftwareCardBackgroundTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardBackgroundTagHelper, SmartSoftwareCardBackgroundTagHelperService>
{
    public SmartSoftwareCardBackgroundType Background { get; set; } = SmartSoftwareCardBackgroundType.Default;

    public SmartSoftwareCardBackgroundTagHelper(SmartSoftwareCardBackgroundTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
