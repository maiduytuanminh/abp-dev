using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

[HtmlTargetElement("ss-button", Attributes = "ss-collapse-id")]
[HtmlTargetElement("a", Attributes = "ss-collapse-id")]
public class SmartSoftwareCollapseButtonTagHelper : SmartSoftwareTagHelper<SmartSoftwareCollapseButtonTagHelper, SmartSoftwareCollapseButtonTagHelperService>
{
    [HtmlAttributeName("ss-collapse-id")]
    public string BodyId { get; set; } = default!;

    public SmartSoftwareCollapseButtonTagHelper(SmartSoftwareCollapseButtonTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
