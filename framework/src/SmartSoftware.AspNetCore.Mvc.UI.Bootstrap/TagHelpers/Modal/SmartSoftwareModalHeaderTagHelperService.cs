using Localization.Resources.SmartSoftwareUi;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class SmartSoftwareModalHeaderTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareModalHeaderTagHelper>
{
    protected IStringLocalizer<SmartSoftwareUiResource> L { get; }

    public SmartSoftwareModalHeaderTagHelperService(IStringLocalizer<SmartSoftwareUiResource> localizer)
    {
        L = localizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("modal-header");
        output.PreContent.SetHtmlContent(CreatePreContent());
        output.PostContent.SetHtmlContent(CreatePostContent());
    }

    protected virtual string CreatePreContent()
    {
        var title = new TagBuilder("h5");
        title.AddCssClass("modal-title");
        title.InnerHtml.AppendHtml(TagHelper.Title);

        return title.ToHtmlString();
    }

    protected virtual string CreatePostContent()
    {
        var button = new TagBuilder("button");
        button.AddCssClass("btn-close");
        button.Attributes.Add("type", "button");
        button.Attributes.Add("data-bs-dismiss", "modal");
        button.Attributes.Add("aria-label", L["Close"].Value);

        return button.ToHtmlString();
    }
}
