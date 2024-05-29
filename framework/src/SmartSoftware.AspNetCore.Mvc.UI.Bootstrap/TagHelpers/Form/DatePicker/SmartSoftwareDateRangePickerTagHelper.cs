using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

[HtmlTargetElement("ss-date-range-picker", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareDateRangePickerTagHelper : SmartSoftwareDatePickerBaseTagHelper<SmartSoftwareDateRangePickerTagHelper>
{
    public ModelExpression? AspForStart { get; set; }

    public ModelExpression? AspForEnd { get; set; }

    public SmartSoftwareDateRangePickerTagHelper(SmartSoftwareDateRangePickerTagHelperService tagHelperService) :
        base(tagHelperService)
    {
    }
}