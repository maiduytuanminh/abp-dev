namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public static class SmartSoftwareButtonSizeExtensions
{
    public static string ToClassName(this SmartSoftwareButtonSize size)
    {
        switch (size)
        {
            case SmartSoftwareButtonSize.Small:
            case SmartSoftwareButtonSize.Block_Small:
                return "btn-sm";
            case SmartSoftwareButtonSize.Medium:
            case SmartSoftwareButtonSize.Block_Medium:
                return "btn-md";
            case SmartSoftwareButtonSize.Large:
            case SmartSoftwareButtonSize.Block_Large:
                return "btn-lg";
            case SmartSoftwareButtonSize.Block:
            case SmartSoftwareButtonSize.Default:
                return "";
            default:
                throw new SmartSoftwareException($"Unknown {nameof(SmartSoftwareButtonSize)}: {size}");
        }
    }
}
