using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

public class SmartSoftwareAlertLinkTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareAlertLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("alert-link");
        output.Attributes.RemoveAll("ss-alert-link");
    }
}
