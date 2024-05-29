
using SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

public class SmartSoftwareTabsTagHelper : SmartSoftwareTagHelper<SmartSoftwareTabsTagHelper, SmartSoftwareTabsTagHelperService>
{
    public string? Name { get; set; }

    public TabStyle TabStyle { get; set; } = TabStyle.Tab;

    public ColumnSize VerticalHeaderSize { get; set; } = ColumnSize._3;

    public SmartSoftwareTabsTagHelper(SmartSoftwareTabsTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
