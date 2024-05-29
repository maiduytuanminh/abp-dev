using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tooltip;

[HtmlTargetElement("button", Attributes = "ss-tooltip")]
[HtmlTargetElement("button", Attributes = "ss-tooltip-right")]
[HtmlTargetElement("button", Attributes = "ss-tooltip-left")]
[HtmlTargetElement("button", Attributes = "ss-tooltip-top")]
[HtmlTargetElement("button", Attributes = "ss-tooltip-bottom")]
[HtmlTargetElement("ss-button", Attributes = "ss-tooltip")]
[HtmlTargetElement("ss-button", Attributes = "ss-tooltip-right")]
[HtmlTargetElement("ss-button", Attributes = "ss-tooltip-left")]
[HtmlTargetElement("ss-button", Attributes = "ss-tooltip-top")]
[HtmlTargetElement("ss-button", Attributes = "ss-tooltip-bottom")]
public class SmartSoftwareTooltipTagHelper : SmartSoftwareTagHelper<SmartSoftwareTooltipTagHelper, SmartSoftwareTooltipTagHelperService>
{
    public string? SmartSoftwareTooltip { get; set; }

    public string? SmartSoftwareTooltipRight { get; set; }

    public string? SmartSoftwareTooltipLeft { get; set; }

    public string? SmartSoftwareTooltipTop { get; set; }

    public string? SmartSoftwareTooltipBottom { get; set; }

    public string? Title { get; set; }

    public SmartSoftwareTooltipTagHelper(SmartSoftwareTooltipTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
