using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

[HtmlTargetElement("span", Attributes = "ss-navbar-text")]
public class SmartSoftwareNavbarTextTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavbarTextTagHelper, SmartSoftwareNavbarTextTagHelperService>
{
    public SmartSoftwareNavbarTextTagHelper(SmartSoftwareNavbarTextTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
