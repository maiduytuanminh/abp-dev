namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavItemTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavItemTagHelper, SmartSoftwareNavItemTagHelperService>
{
    public bool? Dropdown { get; set; }

    public SmartSoftwareNavItemTagHelper(SmartSoftwareNavItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
