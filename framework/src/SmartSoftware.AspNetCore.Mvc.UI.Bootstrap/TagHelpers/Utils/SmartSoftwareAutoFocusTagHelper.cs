using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Utils;

[HtmlTargetElement(Attributes = "ss-auto-focus")]
public class SmartSoftwareAutoFocusTagHelper : SmartSoftwareTagHelper
{
    [HtmlAttributeName("ss-auto-focus")]
    public bool AutoFocus { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (AutoFocus)
        {
            output.Attributes.Add("data-auto-focus", "true");
        }
    }
}
