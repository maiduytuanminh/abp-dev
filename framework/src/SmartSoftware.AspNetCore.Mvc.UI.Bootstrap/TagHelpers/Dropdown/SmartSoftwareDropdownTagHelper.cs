namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class SmartSoftwareDropdownTagHelper : SmartSoftwareTagHelper<SmartSoftwareDropdownTagHelper, SmartSoftwareDropdownTagHelperService>
{
    public DropdownDirection Direction { get; set; } = DropdownDirection.Down;

    public SmartSoftwareDropdownTagHelper(SmartSoftwareDropdownTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
