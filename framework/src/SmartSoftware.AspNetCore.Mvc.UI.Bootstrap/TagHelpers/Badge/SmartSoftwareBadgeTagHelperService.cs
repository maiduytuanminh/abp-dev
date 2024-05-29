using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge;

public class SmartSoftwareBadgeTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareBadgeTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetBadgeClass(context, output);
        SetBadgeStyle(context, output);
    }

    protected virtual void SetBadgeStyle(TagHelperContext context, TagHelperOutput output)
    {
        var badgeType = GetBadgeType(context, output);

        if (badgeType != SmartSoftwareBadgeType.Default && badgeType != SmartSoftwareBadgeType._)
        {
            output.Attributes.AddClass("bg-" + badgeType.ToString().ToLowerInvariant());
        }
    }

    protected virtual void SetBadgeClass(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("badge");

        if (TagHelper.BadgePillType != SmartSoftwareBadgeType._)
        {
            output.Attributes.AddClass("rounded-pill");
        }
    }

    protected virtual SmartSoftwareBadgeType GetBadgeType(TagHelperContext context, TagHelperOutput output)
    {
        return TagHelper.BadgeType != SmartSoftwareBadgeType._ ? TagHelper.BadgeType : TagHelper.BadgePillType;
    }
}
