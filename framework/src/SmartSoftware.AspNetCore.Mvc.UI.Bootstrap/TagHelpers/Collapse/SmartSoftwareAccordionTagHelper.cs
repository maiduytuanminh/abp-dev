namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class SmartSoftwareAccordionTagHelper : SmartSoftwareTagHelper<SmartSoftwareAccordionTagHelper, SmartSoftwareAccordionTagHelperService>
{
    public string? Id { get; set; }

    public SmartSoftwareAccordionTagHelper(SmartSoftwareAccordionTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
