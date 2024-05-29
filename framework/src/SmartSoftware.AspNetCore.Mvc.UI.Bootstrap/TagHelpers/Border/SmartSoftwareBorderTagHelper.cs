using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

[HtmlTargetElement(Attributes = "ss-border")]
public class SmartSoftwareBorderTagHelper : SmartSoftwareTagHelper<SmartSoftwareBorderTagHelper, SmartSoftwareBorderTagHelperService>
{
    public SmartSoftwareBorderType SmartSoftwareBorder { get; set; } = SmartSoftwareBorderType.Default;

    public SmartSoftwareBorderTagHelper(SmartSoftwareBorderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
