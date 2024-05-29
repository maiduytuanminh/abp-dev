namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class SmartSoftwareAccordionItemTagHelper : SmartSoftwareTagHelper<SmartSoftwareAccordionItemTagHelper, SmartSoftwareAccordionItemTagHelperService>
{
    public string? Id { get; set; }

    public string Title { get; set; } = default!;

    public bool? Active { get; set; }

    public SmartSoftwareAccordionItemTagHelper(SmartSoftwareAccordionItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
