namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public static class SmartSoftwareModalSizeExtensions
{
    public static string ToClassName(this SmartSoftwareModalSize size)
    {
        switch (size)
        {
            case SmartSoftwareModalSize.Small:
                return "modal-sm";
            case SmartSoftwareModalSize.Large:
                return "modal-lg";
            case SmartSoftwareModalSize.ExtraLarge:
                return "modal-xl";
            case SmartSoftwareModalSize.Default:
                return "";
            case SmartSoftwareModalSize.Fullscreen:
                return "modal-fullscreen";
            case SmartSoftwareModalSize.FullscreenSmDown:
                return "modal-fullscreen-sm-down";
            case SmartSoftwareModalSize.FullscreenMdDown:
                return "modal-fullscreen-md-down";
            case SmartSoftwareModalSize.FullscreenLgDown:
                return "modal-fullscreen-lg-down";
            case SmartSoftwareModalSize.FullscreenXlDown:
                return "modal-fullscreen-xl-down";
            case SmartSoftwareModalSize.FullscreenXxlDown:
                return "modal-fullscreen-xxl-down";
            default:
                throw new SmartSoftwareException($"Unknown {nameof(SmartSoftwareModalSize)}: {size}");
        }
    }
}
