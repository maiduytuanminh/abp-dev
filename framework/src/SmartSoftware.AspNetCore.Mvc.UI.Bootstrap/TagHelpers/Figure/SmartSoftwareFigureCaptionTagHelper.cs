using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure;

[HtmlTargetElement("ss-figcaption")]
public class SmartSoftwareFigureCaptionTagHelper : SmartSoftwareTagHelper<SmartSoftwareFigureCaptionTagHelper, SmartSoftwareFigureCaptionTagHelperService>
{
    public SmartSoftwareFigureCaptionTagHelper(SmartSoftwareFigureCaptionTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
