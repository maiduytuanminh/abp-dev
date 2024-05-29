namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class SmartSoftwareCardTagHelper : SmartSoftwareTagHelper<SmartSoftwareCardTagHelper, SmartSoftwareCardTagHelperService>
{
    public SmartSoftwareCardBorderColorType Border { get; set; } = SmartSoftwareCardBorderColorType.Default;

    public bool AddMarginBottomClass  { get; set; } = true;

    public SmartSoftwareCardTagHelper(SmartSoftwareCardTagHelperService tagHelperService)
        : base(tagHelperService)
    {
    }
}
