﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class SmartSoftwareListGroupTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareListGroupTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.Attributes.AddClass("list-group");

        if (TagHelper.Flush ?? false)
        {
            output.Attributes.AddClass("list-group-flush");
        }
    }
}
