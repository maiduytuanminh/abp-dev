using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

[HtmlTargetElement(Attributes = "ss-nav-link")]
public class SmartSoftwareNavLinkTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavLinkTagHelper, SmartSoftwareNavLinkTagHelperService>
{
    public bool? Active { get; set; }

    public bool? Disabled { get; set; }

    public SmartSoftwareNavLinkTagHelper(SmartSoftwareNavLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
