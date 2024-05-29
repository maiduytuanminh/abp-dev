using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

[HtmlTargetElement("thead")]
public class SmartSoftwareTableHeaderTagHelper : SmartSoftwareTagHelper<SmartSoftwareTableHeaderTagHelper, SmartSoftwareTableHeaderTagHelperService>
{
    public SmartSoftwareTableHeaderTheme Theme { get; set; } = SmartSoftwareTableHeaderTheme.Default;

    public SmartSoftwareTableHeaderTagHelper(SmartSoftwareTableHeaderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
