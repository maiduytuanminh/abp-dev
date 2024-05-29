using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[HtmlTargetElement(Attributes = "asp-validation-for")]
[HtmlTargetElement(Attributes = "asp-validation-summary")]
public class SmartSoftwareValidationAttributeTagHelper : SmartSoftwareTagHelper<SmartSoftwareValidationAttributeTagHelper, SmartSoftwareValidationAttributeTagHelperService>, ITransientDependency
{
    public SmartSoftwareValidationAttributeTagHelper(SmartSoftwareValidationAttributeTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
