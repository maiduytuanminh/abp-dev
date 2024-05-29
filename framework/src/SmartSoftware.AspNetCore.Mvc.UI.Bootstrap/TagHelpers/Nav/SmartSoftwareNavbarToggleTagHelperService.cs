using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavbarToggleTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareNavbarToggleTagHelper>
{
    protected IStringLocalizer<SmartSoftwareUiResource> L { get; }

    public SmartSoftwareNavbarToggleTagHelperService(IStringLocalizer<SmartSoftwareUiResource> stringLocalizer)
    {
        L = stringLocalizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetRandomNameIfNotProvided();
        output.TagName = "div";
        output.Attributes.AddClass("collapse");
        output.Attributes.AddClass("navbar-collapse");
        output.Attributes.Add("id", TagHelper.Id);
        SetToggleButton(context, output);
    }

    protected virtual void SetToggleButton(TagHelperContext context, TagHelperOutput output)
    {
        var span = new TagBuilder("span");
        span.AddCssClass("navbar-toggler-icon");

        var button = new TagBuilder("button");
        button.AddCssClass("navbar-toggler");
        button.Attributes.Add("type", "button");
        button.Attributes.Add("data-bs-toggle", "collapse");
        button.Attributes.Add("data-bs-target", "#" + TagHelper.Id);
        button.Attributes.Add("aria-controls", TagHelper.Id);
        button.Attributes.Add("aria-expanded", "false");
        button.Attributes.Add("aria-label", L["ToggleNavigation"].Value);
        button.InnerHtml.AppendHtml(span);

        output.PreElement.SetHtmlContent(button);
    }

    protected virtual void SetRandomNameIfNotProvided()
    {
        if (string.IsNullOrWhiteSpace(TagHelper.Id))
        {
            TagHelper.Id = "N" + Guid.NewGuid().ToString("N");
        }
    }
}
