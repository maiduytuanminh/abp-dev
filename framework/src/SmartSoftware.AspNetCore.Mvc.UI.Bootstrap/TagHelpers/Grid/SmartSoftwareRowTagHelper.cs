using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

[HtmlTargetElement("ss-row")]
[HtmlTargetElement("ss-form-row")]
public class SmartSoftwareRowTagHelper : SmartSoftwareTagHelper<SmartSoftwareRowTagHelper, SmartSoftwareRowTagHelperService>
{
    public VerticalAlign VAlign { get; set; } = VerticalAlign.Default;

    public HorizontalAlign HAlign { get; set; } = HorizontalAlign.Default;

    public bool? Gutters { get; set; } = true;

    public SmartSoftwareRowTagHelper(SmartSoftwareRowTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
