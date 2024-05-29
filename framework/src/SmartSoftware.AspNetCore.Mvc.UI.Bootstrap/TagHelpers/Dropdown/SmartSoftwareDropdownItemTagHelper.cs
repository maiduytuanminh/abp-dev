namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class SmartSoftwareDropdownItemTagHelper : SmartSoftwareTagHelper<SmartSoftwareDropdownItemTagHelper, SmartSoftwareDropdownItemTagHelperService>
{
    public bool? Active { get; set; }

    public bool? Disabled { get; set; }

    public SmartSoftwareDropdownItemTagHelper(SmartSoftwareDropdownItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
