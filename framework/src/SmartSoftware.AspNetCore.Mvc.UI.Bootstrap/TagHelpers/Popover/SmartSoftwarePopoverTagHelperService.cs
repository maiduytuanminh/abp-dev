using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Web;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Popover;

public class SmartSoftwarePopoverTagHelperService : SmartSoftwareTagHelperService<SmartSoftwarePopoverTagHelper>
{
    protected IHtmlGenerator HtmlGenerator { get; }

    public SmartSoftwarePopoverTagHelperService(IHtmlGenerator htmlGenerator)
    {
        HtmlGenerator = htmlGenerator;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!TagHelper.Disabled ?? true)
        {
            SetDataToggle(context, output);
            SetDataPlacement(context, output);
            SetPopoverData(context, output);
            SetDataTriggerIfDismissible(context, output);
            SetDataTriggerIfHoverable(context, output);
        }
        else
        {
            SetDisabled(context, output);
        }
    }

    protected virtual void SetDisabled(TagHelperContext context, TagHelperOutput output)
    {
        var triggerValue = TagHelper.Dismissible ?? false ? "focus" : "";
        if (TagHelper.Hoverable ?? false)
        {
            if (triggerValue.Contains("focus"))
            {
                triggerValue = triggerValue.Replace("focus", "focus hover");
            }
            else
            {
                triggerValue = "hover";
            }
        }

        var dataPlacement = GetDirectory().ToString().ToLowerInvariant();
        // data-bs-placement="default" with data-trigger="focus" causes Cannot read property 'indexOf' of undefined at computeAutoPlacement(bootstrap.bundle.js?_v=637146714627330435:2185) error
        if (IsDismissibleOrHoverable() && GetDirectory() == PopoverDirectory.Default)
        {
            //dataPlacementAsHtml = string.Empty; //bootstrap default placement is right, ss's is top.
            dataPlacement = dataPlacement.Replace("default", "top");
        }

        var titleAttribute = output.Attributes.FirstOrDefault(at => at.Name == "title");

        var span = new TagBuilder("span");
        span.AddCssClass("d-inline-block");
        span.Attributes.Add("tabindex", "0");
        span.Attributes.Add("data-bs-toggle", "popover");
        span.Attributes.Add("data-bs-content", GetDataContent());
        span.Attributes.Add("data-bs-trigger", triggerValue);
        span.Attributes.Add("data-bs-placement", dataPlacement);

        if (titleAttribute != null)
        {
            span.Attributes.Add("title", HttpUtility.HtmlDecode(titleAttribute.Value.ToString()));
        }

        output.PreElement.SetHtmlContent(span.RenderStartTag());
        output.PostElement.SetHtmlContent(span.RenderEndTag());

        output.Attributes.Add("style", "pointer-events: none;");
    }

    protected virtual void SetDataTriggerIfDismissible(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Dismissible ?? false)
        {
            output.Attributes.Add("data-bs-trigger", "focus");
        }
    }

    protected virtual void SetDataTriggerIfHoverable(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Hoverable ?? false)
        {
            //If already has focus data trigger
            if (output.Attributes.TryGetAttribute("data-bs-trigger", out _))
            {
                output.Attributes.SetAttribute(new TagHelperAttribute("data-bs-trigger", "focus hover"));
            }
            else
            {
                output.Attributes.Add("data-bs-trigger", "hover");
            }
        }
    }

    protected virtual void SetDataToggle(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("data-bs-toggle", "popover");
    }

    protected virtual void SetDataPlacement(TagHelperContext context, TagHelperOutput output)
    {
        var directory = GetDirectory();
        if (directory == PopoverDirectory.Default)
        {
            directory = PopoverDirectory.Bottom;
        }
        output.Attributes.Add("data-bs-placement", directory.ToString().ToLowerInvariant());
    }

    protected virtual void SetPopoverData(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("data-bs-content", GetDataContent());
    }

    protected virtual string GetDataContent()
    {
        switch (GetDirectory())
        {
            case PopoverDirectory.Top:
                return TagHelper.SmartSoftwarePopoverTop!;
            case PopoverDirectory.Right:
                return TagHelper.SmartSoftwarePopoverRight!;
            case PopoverDirectory.Bottom:
                return TagHelper.SmartSoftwarePopoverBottom!;
            case PopoverDirectory.Left:
                return TagHelper.SmartSoftwarePopoverLeft!;
            default:
                return TagHelper.SmartSoftwarePopover!;
        }
    }

    protected virtual PopoverDirectory GetDirectory()
    {
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwarePopoverTop))
        {
            return PopoverDirectory.Top;
        }
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwarePopoverBottom))
        {
            return PopoverDirectory.Bottom;
        }
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwarePopoverRight))
        {
            return PopoverDirectory.Right;
        }
        if (!string.IsNullOrWhiteSpace(TagHelper.SmartSoftwarePopoverLeft))
        {
            return PopoverDirectory.Left;
        }

        return PopoverDirectory.Default;
    }
    protected virtual bool IsDismissibleOrHoverable()
    {
        if (TagHelper.Dismissible ?? false)
        {
            return true;
        }
        if (TagHelper.Hoverable ?? false)
        {
            return true;
        }
        return false;
    }
}
