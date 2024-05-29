using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure;

[HtmlTargetElement("ss-image", ParentTag = "ss-figure")]
public class SmartSoftwareFigureImageTagHelper : SmartSoftwareTagHelper<SmartSoftwareFigureImageTagHelper, SmartSoftwareFigureImageTagHelperService>
{
    public SmartSoftwareFigureImageTagHelper(SmartSoftwareFigureImageTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
