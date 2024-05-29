using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

[HtmlTargetElement(Attributes = "ss-rounded")]
public class SmartSoftwareRoundedTagHelper : SmartSoftwareTagHelper<SmartSoftwareRoundedTagHelper, SmartSoftwareRoundedTagHelperService>
{
    public SmartSoftwareRoundedType SmartSoftwareRounded { get; set; } = SmartSoftwareRoundedType.Default;

    public SmartSoftwareRoundedTagHelper(SmartSoftwareRoundedTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
