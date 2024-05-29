namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardTitleTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardTitleTagHelper, SmartSoftwareCardTitleTagHelperService>
{
    public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H5;

    public HtmlHeadingType Heading { get; set; } = DefaultHeading;

    public SmartSoftwareCardTitleTagHelper(SmartSoftwareCardTitleTagHelperService tagHelperService)
        : base(tagHelperService)
    {
    }
}
