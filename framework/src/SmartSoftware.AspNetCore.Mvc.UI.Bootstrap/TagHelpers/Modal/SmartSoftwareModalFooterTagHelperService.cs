﻿using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Text;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class SmartSoftwareModalFooterTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareModalFooterTagHelper>
{
    private readonly IStringLocalizer<SmartSoftwareUiResource> _localizer;

    public SmartSoftwareModalFooterTagHelperService(IStringLocalizer<SmartSoftwareUiResource> localizer)
    {
        _localizer = localizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("modal-footer");

        if (TagHelper.Buttons != SmartSoftwareModalButtons.None)
        {
            output.PostContent.SetHtmlContent(CreateContent());
        }

        ProcessButtonsAlignment(output);
    }

    protected virtual string CreateContent()
    {
        var sb = new StringBuilder();

        switch (TagHelper.Buttons)
        {
            case SmartSoftwareModalButtons.Cancel:
                sb.AppendLine(GetCancelButton());
                break;
            case SmartSoftwareModalButtons.Close:
                sb.AppendLine(GetCloseButton());
                break;
            case SmartSoftwareModalButtons.Save:
                sb.AppendLine(GetSaveButton());
                break;
            case SmartSoftwareModalButtons.Save | SmartSoftwareModalButtons.Cancel:
                sb.AppendLine(GetCancelButton());
                sb.AppendLine(GetSaveButton());
                break;
            case SmartSoftwareModalButtons.Save | SmartSoftwareModalButtons.Close:
                sb.AppendLine(GetCloseButton());
                sb.AppendLine(GetSaveButton());
                break;
        }

        return sb.ToString();
    }

    protected virtual string GetSaveButton()
    {
        var icon = new TagBuilder("i");
        icon.AddCssClass("fa");
        icon.AddCssClass("fa-check");

        var span = new TagBuilder("span");
        span.InnerHtml.Append(_localizer["Save"]);

        var element = new TagBuilder("button");
        element.Attributes.Add("type", "submit");
        element.AddCssClass("btn");
        element.AddCssClass("btn-primary");
        element.Attributes.Add("data-busy-text", _localizer["SavingWithThreeDot"]);
        element.InnerHtml.AppendHtml(icon);
        element.InnerHtml.AppendHtml(" ");
        element.InnerHtml.AppendHtml(span);

        return element.ToHtmlString();
    }

    protected virtual string GetCloseButton()
    {
        var element = new TagBuilder("button");
        element.Attributes.Add("type", "button");
        element.Attributes.Add("data-bs-dismiss", "modal");
        element.AddCssClass("btn");
        element.AddCssClass("btn-secondary");
        element.InnerHtml.Append(_localizer["Close"]);

        return element.ToHtmlString();
    }

    protected virtual string GetCancelButton(bool isDestructive = false)
    {
        var element = new TagBuilder("button");
        element.Attributes.Add("type", "button");
        element.Attributes.Add("data-bs-dismiss", "modal");
        element.AddCssClass("btn");
        element.AddCssClass("btn-outline-primary");
        element.InnerHtml.Append(_localizer["Cancel"]);

        return element.ToHtmlString();
    }

    protected virtual void ProcessButtonsAlignment(TagHelperOutput output)
    {
        if (TagHelper.ButtonAlignment == ButtonsAlign.Default)
        {
            return;
        }
        output.Attributes.AddClass("justify-content-" + TagHelper.ButtonAlignment.ToString().ToLowerInvariant());
    }
}
