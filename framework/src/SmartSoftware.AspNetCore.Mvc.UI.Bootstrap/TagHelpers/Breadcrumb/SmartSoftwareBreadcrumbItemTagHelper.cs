namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Breadcrumb;

public class SmartSoftwareBreadcrumbItemTagHelper : SmartSoftwareTagHelper<SmartSoftwareBreadcrumbItemTagHelper, SmartSoftwareBreadcrumbItemTagHelperService>
{
    public string? Href { get; set; }

    public string Title { get; set; } = default!;

    public bool Active { get; set; }

    public SmartSoftwareBreadcrumbItemTagHelper(SmartSoftwareBreadcrumbItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
