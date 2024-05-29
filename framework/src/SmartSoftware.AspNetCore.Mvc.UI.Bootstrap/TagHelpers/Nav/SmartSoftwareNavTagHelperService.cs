using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareNavTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.AddClass("nav");
        SetAlign(context, output);
        SetNavStyle(context, output);
    }

    protected virtual void SetAlign(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Align == SmartSoftwareNavAlign.Default)
        {
            return;
        }

        output.Attributes.AddClass("justify-content-" + TagHelper.Align.ToString().ToLowerInvariant());
    }

    protected virtual void SetNavStyle(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.NavStyle)
        {
            case NavStyle.Default:
                return;
            case NavStyle.Pill:
                output.Attributes.AddClass("nav-pills");
                break;
            case NavStyle.Vertical:
                output.Attributes.AddClass("flex-column");
                break;
            case NavStyle.PillVertical:
                output.Attributes.AddClass("nav-pills");
                output.Attributes.AddClass("flex-column");
                break;
        }
    }
}
