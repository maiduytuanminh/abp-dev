using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

public class SmartSoftwareRoundedTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareRoundedTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var roundedClass = "rounded";

        if (TagHelper.SmartSoftwareRounded != SmartSoftwareRoundedType.Default)
        {
            roundedClass += "-" + TagHelper.SmartSoftwareRounded.ToString().ToLowerInvariant().Replace("_", "");
        }

        output.Attributes.AddClass(roundedClass);
    }
}
