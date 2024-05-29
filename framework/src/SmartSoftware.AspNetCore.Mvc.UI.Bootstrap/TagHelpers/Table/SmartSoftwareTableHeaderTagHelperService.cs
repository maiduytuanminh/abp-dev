using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

public class SmartSoftwareTableHeaderTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareTableHeaderTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetTheme(context, output);
    }

    protected virtual void SetTheme(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Theme)
        {
            case SmartSoftwareTableHeaderTheme.Default:
                return;
            case SmartSoftwareTableHeaderTheme.Dark:
                output.Attributes.AddClass("thead-dark");
                return;
            case SmartSoftwareTableHeaderTheme.Light:
                output.Attributes.AddClass("thead-light");
                return;
        }
    }
}
