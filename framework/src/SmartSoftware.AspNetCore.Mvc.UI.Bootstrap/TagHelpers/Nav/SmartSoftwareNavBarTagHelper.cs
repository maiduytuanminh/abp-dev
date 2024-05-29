namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavBarTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavBarTagHelper, SmartSoftwareNavBarTagHelperService>
{
    public SmartSoftwareNavbarSize Size { get; set; } = SmartSoftwareNavbarSize.Default;

    public SmartSoftwareNavbarStyle NavbarStyle { get; set; } = SmartSoftwareNavbarStyle.Default;

    public SmartSoftwareNavBarTagHelper(SmartSoftwareNavBarTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
