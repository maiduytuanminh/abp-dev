namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class SmartSoftwareCollapseBodyTagHelper : SmartSoftwareTagHelper<SmartSoftwareCollapseBodyTagHelper, SmartSoftwareCollapseBodyTagHelperService>
{
    public string? Id { get; set; }

    public bool? Multi { get; set; }

    public bool? Show { get; set; }

    public SmartSoftwareCollapseBodyTagHelper(SmartSoftwareCollapseBodyTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
