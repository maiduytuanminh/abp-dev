namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class SmartSoftwareListGroupTagHelper : SmartSoftwareTagHelper<SmartSoftwareListGroupTagHelper, SmartSoftwareListGroupTagHelperService>
{
    public bool? Flush { get; set; }

    public SmartSoftwareListGroupTagHelper(SmartSoftwareListGroupTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
