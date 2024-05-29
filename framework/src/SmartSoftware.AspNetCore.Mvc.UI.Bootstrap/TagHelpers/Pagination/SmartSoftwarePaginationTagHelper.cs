using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;

[HtmlTargetElement("ss-paginator")]
public class SmartSoftwarePaginationTagHelper : SmartSoftwareTagHelper<SmartSoftwarePaginationTagHelper, SmartSoftwarePaginationTagHelperService>
{
    public PagerModel Model { get; set; } = default!;

    public bool? ShowInfo { get; set; }

    public SmartSoftwarePaginationTagHelper(SmartSoftwarePaginationTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
