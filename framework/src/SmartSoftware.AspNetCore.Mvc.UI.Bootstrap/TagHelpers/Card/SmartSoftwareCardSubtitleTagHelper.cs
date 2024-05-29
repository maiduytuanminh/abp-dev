namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardSubtitleTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardSubtitleTagHelper, SmartSoftwareCardSubtitleTagHelperService>
{
    public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H6;

    public HtmlHeadingType Heading { get; set; } = DefaultHeading;

    public SmartSoftwareCardSubtitleTagHelper(SmartSoftwareCardSubtitleTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
