using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

[HtmlTargetElement("ss-tab-link", TagStructure = TagStructure.WithoutEndTag)]
public class SmartSoftwareTabLinkTagHelper : SmartSoftwareTagHelper<SmartSoftwareTabLinkTagHelper, SmartSoftwareTabLinkTagHelperService>
{
    public string? Name { get; set; }

    public string Title { get; set; } = default!;

    public string? ParentDropdownName { get; set; }

    public string Href { get; set; } = default!;

    public SmartSoftwareTabLinkTagHelper(SmartSoftwareTabLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
