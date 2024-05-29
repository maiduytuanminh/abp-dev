using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

[HtmlTargetElement(Attributes = "ss-navbar-brand")]
public class SmartSoftwareNavbarBrandTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavbarBrandTagHelper, SmartSoftwareNavbarBrandTagHelperService>
{

    public SmartSoftwareNavbarBrandTagHelper(SmartSoftwareNavbarBrandTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
