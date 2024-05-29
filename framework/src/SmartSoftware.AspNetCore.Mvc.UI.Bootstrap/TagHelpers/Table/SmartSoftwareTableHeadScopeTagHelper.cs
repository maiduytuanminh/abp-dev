using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

[HtmlTargetElement("th")]
public class SmartSoftwareTableHeadScopeTagHelper : SmartSoftwareTagHelper<SmartSoftwareTableHeadScopeTagHelper, SmartSoftwareTableHeadScopeTagHelperService>
{
    public SmartSoftwareThScope Scope { get; set; } = SmartSoftwareThScope.Default;

    public SmartSoftwareTableHeadScopeTagHelper(SmartSoftwareTableHeadScopeTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
