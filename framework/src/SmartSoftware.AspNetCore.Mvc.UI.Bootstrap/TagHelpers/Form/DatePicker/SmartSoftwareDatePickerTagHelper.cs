using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

[HtmlTargetElement("ss-date-picker", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareDatePickerTagHelper : SmartSoftwareDatePickerBaseTagHelper<SmartSoftwareDatePickerTagHelper>
{
    public ModelExpression? AspFor { get; set; }
    
    public SmartSoftwareDatePickerTagHelper(SmartSoftwareDatePickerTagHelperService service) : base(service)
    {
    }
}