namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class SmartSoftwareDropdownMenuTagHelper : SmartSoftwareTagHelper<SmartSoftwareDropdownMenuTagHelper, SmartSoftwareDropdownMenuTagHelperService>
{
    public DropdownAlign Align { get; set; } = DropdownAlign.Start;

    public SmartSoftwareDropdownMenuTagHelper(SmartSoftwareDropdownMenuTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
