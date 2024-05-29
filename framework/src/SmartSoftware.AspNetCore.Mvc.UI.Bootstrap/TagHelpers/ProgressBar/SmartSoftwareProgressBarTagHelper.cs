using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ProgressBar;

[HtmlTargetElement("ss-progress-bar")]
[HtmlTargetElement("ss-progress-part")]
public class SmartSoftwareProgressBarTagHelper : SmartSoftwareTagHelper<SmartSoftwareProgressBarTagHelper, SmartSoftwareProgressBarTagHelperService>
{
    public double Value { get; set; }

    public double MinValue { get; set; } = 0;

    public double MaxValue { get; set; } = 100;

    public SmartSoftwareProgressBarType Type { get; set; } = SmartSoftwareProgressBarType.Default;

    public bool? Strip { get; set; }

    public bool? Animation { get; set; }

    public SmartSoftwareProgressBarTagHelper(SmartSoftwareProgressBarTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
