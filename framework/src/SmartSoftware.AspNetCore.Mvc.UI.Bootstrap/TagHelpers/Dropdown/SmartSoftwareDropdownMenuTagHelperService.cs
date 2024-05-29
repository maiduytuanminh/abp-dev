﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class SmartSoftwareDropdownMenuTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareDropdownMenuTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("dropdown-menu");
        output.TagMode = TagMode.StartTagAndEndTag;

        SetAlign(context, output);
    }

    protected virtual void SetAlign(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Align)
        {
            case DropdownAlign.End:
                output.Attributes.AddClass("dropdown-menu-end");
                return;
            case DropdownAlign.Start:
                return;
        }
    }
}
