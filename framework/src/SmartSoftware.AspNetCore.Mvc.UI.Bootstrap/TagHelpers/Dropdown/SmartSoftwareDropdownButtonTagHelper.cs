using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class SmartSoftwareDropdownButtonTagHelper : SmartSoftwareTagHelper<SmartSoftwareDropdownButtonTagHelper, SmartSoftwareDropdownButtonTagHelperService>
{
    public string? Text { get; set; }

    public SmartSoftwareButtonSize Size { get; set; } = SmartSoftwareButtonSize.Default;

    public DropdownStyle DropdownStyle { get; set; } = DropdownStyle.Single;

    public SmartSoftwareButtonType ButtonType { get; set; } = SmartSoftwareButtonType.Default;

    public string? Icon { get; set; }

    public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

    public bool? Link { get; set; }

    public bool? NavLink { get; set; }

    public SmartSoftwareDropdownButtonTagHelper(SmartSoftwareDropdownButtonTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
