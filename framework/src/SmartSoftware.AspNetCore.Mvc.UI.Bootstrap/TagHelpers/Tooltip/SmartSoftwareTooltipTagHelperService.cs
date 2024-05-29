using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tooltip;

public class SmartSoftwareTooltipTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareTooltipTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (IsButtonDisabled(context, output))
        {
            SetParentElementWithTooltip(context, output);
            return;
        }

        SetDataToggle(context, output);
        SetDataPlacement(context, output);
        SetTooltipTitle(context, output);
    }

    protected virtual void SetParentElementWithTooltip(TagHelperContext context, TagHelperOutput output)
    {
        var directory = GetDirectory() != TooltipDirectory.Default ? GetDirectory() : TooltipDirectory.Top;
        output.Attributes.Add("data-bs-placement", directory.ToString().ToLowerInvariant());

        var span = new TagBuilder("span");
        span.AddCssClass("d-inline-block");
        span.Attributes.Add("tabindex", "0");
        span.Attributes.Add("data-bs-toggle", "tooltip");
        span.Attributes.Add("data-bs-placement", directory.ToString().ToLowerInvariant());
        span.Attributes.Add("title", GetTitle());

        output.PreElement.SetHtmlContent(span.RenderStartTag());

        output.PostElement.SetHtmlContent(span.RenderEndTag());

        output.Attributes.Add("style", "pointer-events: none;");
    }

    protected virtual void SetDataToggle(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("data-bs-toggle", "tooltip");
    }

    protected virtual void SetDataPlacement(TagHelperContext context, TagHelperOutput output)
    {
        var directory = GetDirectory() != TooltipDirectory.Default ? GetDirectory() : TooltipDirectory.Top;
        output.Attributes.Add("data-bs-placement", directory.ToString().ToLowerInvariant());
    }

    protected virtual void SetTooltipTitle(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("title", GetTitle());
    }

    protected virtual string GetTitle()
    {
        switch (GetDirectory())
        {
            case TooltipDirectory.Top:
                return TagHelper.SmartSoftwareTooltipTop!;
            case TooltipDirectory.Right:
                return TagHelper.SmartSoftwareTooltipRight!;
            case TooltipDirectory.Bottom:
                return TagHelper.SmartSoftwareTooltipBottom!;
            case TooltipDirectory.Left:
                return TagHelper.SmartSoftwareTooltipLeft!;
            default:
                return TagHelper.SmartSoftwareTooltip!;
        }
    }

    protected virtual TooltipDirectory GetDirectory()
    {
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwareTooltipTop))
        {
            return TooltipDirectory.Top;
        }
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwareTooltipBottom))
        {
            return TooltipDirectory.Bottom;
        }
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwareTooltipRight))
        {
            return TooltipDirectory.Right;
        }
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwareTooltipLeft))
        {
            return TooltipDirectory.Left;
        }

        return TooltipDirectory.Default;
    }

    protected virtual bool IsButtonDisabled(TagHelperContext context, TagHelperOutput output)
    {
        return output.Attributes.Any(a => a.Name == "disabled");
    }
}
