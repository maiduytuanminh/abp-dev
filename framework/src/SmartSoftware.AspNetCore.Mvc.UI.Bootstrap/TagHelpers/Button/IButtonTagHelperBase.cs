namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public interface IButtonTagHelperBase
{
    SmartSoftwareButtonType ButtonType { get; }

    SmartSoftwareButtonSize Size { get; }

    string? Text { get; }

    string? Icon { get; }

    bool? Disabled { get; }

    FontIconType IconType { get; }
}
