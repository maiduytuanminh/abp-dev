using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavbarTextTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareNavbarTextTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("navbar-text");
        output.Attributes.RemoveAll("ss-navbar-text");
    }
}
