using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Utils;

[HtmlTargetElement(Attributes = "ss-if")]
public class SmartSoftwareIfTagHelper : SmartSoftwareTagHelper
{
    [HtmlAttributeName("ss-if")]
    public bool Condition { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!Condition)
        {
            output.SuppressOutput();
        }
    }
}
