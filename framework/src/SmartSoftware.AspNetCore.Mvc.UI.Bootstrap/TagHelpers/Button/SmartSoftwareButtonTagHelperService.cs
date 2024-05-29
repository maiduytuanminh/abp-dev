using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class SmartSoftwareButtonTagHelperService : SmartSoftwareButtonTagHelperServiceBase<SmartSoftwareButtonTagHelper>
{
    protected const string DataBusyTextAttributeName = "data-busy-text";
    protected const string DataBusyTextIsHtmlAttributeName = "data-busy-text-is-html";

    protected IStringLocalizer<SmartSoftwareUiResource> L { get; }

    public SmartSoftwareButtonTagHelperService(IStringLocalizer<SmartSoftwareUiResource> localizer)
    {
        L = localizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);
        output.TagName = "button";
        AddType(context, output);
        AddBusyText(context, output);
        AddBusyTextIsHtml(context, output);
    }

    protected virtual void AddType(TagHelperContext context, TagHelperOutput output)
    {
        if (output.Attributes.ContainsName("type"))
        {
            return;
        }

        output.Attributes.Add("type", "button");
    }

    protected virtual void AddBusyText(TagHelperContext context, TagHelperOutput output)
    {
        var busyText = TagHelper.BusyText ?? L["ProcessingWithThreeDot"];
        if (busyText.IsNullOrWhiteSpace())
        {
            return;
        }

        output.Attributes.SetAttribute(DataBusyTextAttributeName, busyText);
    }

    protected virtual void AddBusyTextIsHtml(TagHelperContext context, TagHelperOutput output)
    {
        if (!TagHelper.BusyTextIsHtml)
        {
            return;
        }

        output.Attributes.SetAttribute(DataBusyTextIsHtmlAttributeName, TagHelper.BusyTextIsHtml.ToString().ToLower());
    }
}
