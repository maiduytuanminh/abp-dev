using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardBackgroundTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareCardBackgroundTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetBackground(context, output);
    }

    protected virtual void SetBackground(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Background == SmartSoftwareCardBackgroundType.Default)
        {
            return;
        }

        output.Attributes.AddClass("bg-" + TagHelper.Background.ToString().ToLowerInvariant());
    }
}
