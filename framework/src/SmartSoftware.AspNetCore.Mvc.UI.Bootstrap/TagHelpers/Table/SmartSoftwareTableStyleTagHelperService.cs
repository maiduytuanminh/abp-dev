using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

public class SmartSoftwareTableStyleTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareTableStyleTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetStyle(context, output);
    }

    protected virtual void SetStyle(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.TableStyle != SmartSoftwareTableStyle.Default)
        {
            output.Attributes.AddClass("table-" + TagHelper.TableStyle.ToString().ToLowerInvariant());
        }
    }
}
