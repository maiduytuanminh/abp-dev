﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavItemTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareNavItemTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.Attributes.AddClass("nav-item");

        SetDropdownClass(context, output);
    }

    protected virtual void SetDropdownClass(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Dropdown ?? false)
        {
            output.Attributes.AddClass("dropdown");
        }
    }
}
