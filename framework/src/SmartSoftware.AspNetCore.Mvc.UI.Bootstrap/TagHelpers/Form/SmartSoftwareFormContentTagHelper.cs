using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[HtmlTargetElement("ss-form-content", TagStructure = TagStructure.WithoutEndTag)]
public class SmartSoftwareFormContentTagHelper : SmartSoftwareTagHelper<SmartSoftwareFormContentTagHelper, SmartSoftwareFormContentTagHelperService>, ITransientDependency
{
    public SmartSoftwareFormContentTagHelper(SmartSoftwareFormContentTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
