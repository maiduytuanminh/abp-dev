namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class SmartSoftwareNavbarToggleTagHelper : SmartSoftwareTagHelper<SmartSoftwareNavbarToggleTagHelper, SmartSoftwareNavbarToggleTagHelperService>
{
    public string? Id { get; set; }

    public SmartSoftwareNavbarToggleTagHelper(SmartSoftwareNavbarToggleTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
