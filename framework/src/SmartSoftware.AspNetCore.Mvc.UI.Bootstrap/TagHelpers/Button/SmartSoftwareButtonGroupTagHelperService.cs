﻿using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class SmartSoftwareButtonGroupTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareButtonGroupTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        AddButtonGroupClass(context, output);
        AddSizeClass(context, output);
        AddAttributes(context, output);

        output.TagName = "div";
    }

    protected virtual void AddSizeClass(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Size)
        {
            case SmartSoftwareButtonGroupSize.Default:
                break;
            case SmartSoftwareButtonGroupSize.Small:
                output.Attributes.AddClass("btn-group-sm");
                break;
            case SmartSoftwareButtonGroupSize.Medium:
                output.Attributes.AddClass("btn-group-md");
                break;
            case SmartSoftwareButtonGroupSize.Large:
                output.Attributes.AddClass("btn-group-lg");
                break;
        }
    }

    protected virtual void AddButtonGroupClass(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Direction)
        {
            case SmartSoftwareButtonGroupDirection.Horizontal:
                output.Attributes.AddClass("btn-group");
                break;
            case SmartSoftwareButtonGroupDirection.Vertical:
                output.Attributes.AddClass("btn-group-vertical");
                break;
            default:
                output.Attributes.AddClass("btn-group");
                break;
        }
    }

    protected virtual void AddAttributes(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("role", "group");
    }
}
