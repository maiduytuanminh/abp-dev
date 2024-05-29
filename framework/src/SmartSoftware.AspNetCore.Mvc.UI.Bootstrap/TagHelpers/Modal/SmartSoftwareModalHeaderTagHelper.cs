namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class SmartSoftwareModalHeaderTagHelper : SmartSoftwareTagHelper<SmartSoftwareModalHeaderTagHelper, SmartSoftwareModalHeaderTagHelperService>
{
    public string Title { get; set; } = default!;

    public SmartSoftwareModalHeaderTagHelper(SmartSoftwareModalHeaderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
