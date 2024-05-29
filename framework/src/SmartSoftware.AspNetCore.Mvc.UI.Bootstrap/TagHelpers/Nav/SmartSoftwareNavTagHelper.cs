namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavTagHelper, SmartSoftwareNavTagHelperService>
{
    public SmartSoftwareNavAlign Align { get; set; } = SmartSoftwareNavAlign.Default;

    public NavStyle NavStyle { get; set; } = NavStyle.Default;

    public SmartSoftwareNavTagHelper(SmartSoftwareNavTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
