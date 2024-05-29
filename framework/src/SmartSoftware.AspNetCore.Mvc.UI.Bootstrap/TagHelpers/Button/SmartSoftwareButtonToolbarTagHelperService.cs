using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class SmartSoftwareButtonToolbarTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareButtonToolbarTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("btn-toolbar");
        output.Attributes.Add("role", "toolbar");
    }
}
