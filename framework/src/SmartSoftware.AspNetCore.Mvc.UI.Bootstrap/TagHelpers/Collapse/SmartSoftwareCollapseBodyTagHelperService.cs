﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class SmartSoftwareCollapseBodyTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareCollapseBodyTagHelper>
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("collapse");
        output.Attributes.Add("id", TagHelper.Id);

        if (TagHelper.Show ?? false)
        {
            output.Attributes.AddClass("show");
        }

        if (TagHelper.Multi ?? false)
        {
            output.Attributes.AddClass("multi-collapse");
        }

        var childContent = await output.GetChildContentAsync();

        output.Content.SetHtmlContent(childContent);
    }
}
