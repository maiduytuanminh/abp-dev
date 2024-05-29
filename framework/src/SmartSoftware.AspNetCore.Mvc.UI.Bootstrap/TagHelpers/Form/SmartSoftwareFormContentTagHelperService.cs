using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

public class SmartSoftwareFormContentTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareFormContentTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Clear();
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetContent(SmartSoftwareFormContentPlaceHolder);
    }
}
