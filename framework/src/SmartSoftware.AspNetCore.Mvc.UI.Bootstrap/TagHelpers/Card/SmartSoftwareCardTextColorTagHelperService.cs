using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardTextColorTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareCardTextColorTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetTextColor(context, output);
    }

    protected virtual void SetTextColor(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.TextColor == SmartSoftwareCardTextColorType.Default)
        {
            return;
        }

        output.Attributes.AddClass("text-" + TagHelper.TextColor.ToString().ToLowerInvariant());
    }
}
