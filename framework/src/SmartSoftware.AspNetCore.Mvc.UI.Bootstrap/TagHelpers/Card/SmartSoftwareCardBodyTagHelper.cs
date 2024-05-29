namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardBodyTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardBodyTagHelper, SmartSoftwareCardBodyTagHelperService>
{
    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public SmartSoftwareCardBodyTagHelper(SmartSoftwareCardBodyTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
