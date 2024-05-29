using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Popover;

[HtmlTargetElement("button", Attributes = "ss-popover")]
[HtmlTargetElement("button", Attributes = "ss-popover-right")]
[HtmlTargetElement("button", Attributes = "ss-popover-left")]
[HtmlTargetElement("button", Attributes = "ss-popover-top")]
[HtmlTargetElement("button", Attributes = "ss-popover-bottom")]
[HtmlTargetElement("ss-button", Attributes = "ss-popover")]
[HtmlTargetElement("ss-button", Attributes = "ss-popover-right")]
[HtmlTargetElement("ss-button", Attributes = "ss-popover-left")]
[HtmlTargetElement("ss-button", Attributes = "ss-popover-top")]
[HtmlTargetElement("ss-button", Attributes = "ss-popover-bottom")]
public class SmartSoftwarePopoverTagHelper : SmartSoftwareTagHelper<SmartSoftwarePopoverTagHelper, SmartSoftwarePopoverTagHelperService>
{
    public bool? Disabled { get; set; }

    public bool? Dismissible { get; set; }

    public bool? Hoverable { get; set; }

    public string? SmartSoftwarePopover { get; set; }

    public string? SmartSoftwarePopoverRight { get; set; }

    public string? SmartSoftwarePopoverLeft { get; set; }

    public string? SmartSoftwarePopoverTop { get; set; }

    public string? SmartSoftwarePopoverBottom { get; set; }

    public SmartSoftwarePopoverTagHelper(SmartSoftwarePopoverTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
