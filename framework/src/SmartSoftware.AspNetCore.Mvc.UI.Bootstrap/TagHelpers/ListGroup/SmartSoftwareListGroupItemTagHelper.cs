namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class SmartSoftwareListGroupItemTagHelper : SmartSoftwareTagHelper<SmartSoftwareListGroupItemTagHelper, SmartSoftwareListGroupItemTagHelperService>
{
    public bool? Active { get; set; }

    public bool? Disabled { get; set; }

    public string? Href { get; set; }

    public SmartSoftwareListItemTagType TagType { get; set; } = SmartSoftwareListItemTagType.Default;

    public SmartSoftwareListItemType Type { get; set; } = SmartSoftwareListItemType.Default;

    public SmartSoftwareListGroupItemTagHelper(SmartSoftwareListGroupItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
