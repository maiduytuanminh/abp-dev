namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class SmartSoftwareButtonGroupTagHelper : SmartSoftwareTagHelper<SmartSoftwareButtonGroupTagHelper, SmartSoftwareButtonGroupTagHelperService>
{
    public SmartSoftwareButtonGroupDirection Direction { get; set; } = SmartSoftwareButtonGroupDirection.Horizontal;

    public SmartSoftwareButtonGroupSize Size { get; set; } = SmartSoftwareButtonGroupSize.Default;

    public SmartSoftwareButtonGroupTagHelper(SmartSoftwareButtonGroupTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
