namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class SmartSoftwareModalTagHelper : SmartSoftwareTagHelper<SmartSoftwareModalTagHelper, SmartSoftwareModalTagHelperService>
{
    public SmartSoftwareModalSize Size { get; set; } = SmartSoftwareModalSize.Default;

    public bool? Centered { get; set; } = false;

    public bool? Scrollable { get; set; } = false;

    public bool? Static { get; set; } = false;

    public SmartSoftwareModalTagHelper(SmartSoftwareModalTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
