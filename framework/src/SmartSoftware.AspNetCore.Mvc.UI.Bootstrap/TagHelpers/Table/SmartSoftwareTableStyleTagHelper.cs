using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

[HtmlTargetElement("tr")]
[HtmlTargetElement("td")]
public class SmartSoftwareTableStyleTagHelper : SmartSoftwareTagHelper<SmartSoftwareTableStyleTagHelper, SmartSoftwareTableStyleTagHelperService>
{
    public SmartSoftwareTableStyle TableStyle { get; set; } = SmartSoftwareTableStyle.Default;

    public SmartSoftwareTableStyleTagHelper(SmartSoftwareTableStyleTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
