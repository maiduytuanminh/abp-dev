using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge;

[HtmlTargetElement("a", Attributes = "ss-badge")]
[HtmlTargetElement("span", Attributes = "ss-badge")]
[HtmlTargetElement("a", Attributes = "ss-badge-pill")]
[HtmlTargetElement("span", Attributes = "ss-badge-pill")]
public class SmartSoftwareBadgeTagHelper : SmartSoftwareTagHelper<SmartSoftwareBadgeTagHelper, SmartSoftwareBadgeTagHelperService>
{
    [HtmlAttributeName("ss-badge")]
    public SmartSoftwareBadgeType BadgeType { get; set; } = SmartSoftwareBadgeType._;

    [HtmlAttributeName("ss-badge-pill")]
    public SmartSoftwareBadgeType BadgePillType { get; set; } = SmartSoftwareBadgeType._;

    public SmartSoftwareBadgeTagHelper(SmartSoftwareBadgeTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
