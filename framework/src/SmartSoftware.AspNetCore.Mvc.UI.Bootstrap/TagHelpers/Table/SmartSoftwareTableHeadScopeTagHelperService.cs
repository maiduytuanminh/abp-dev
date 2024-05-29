using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

public class SmartSoftwareTableHeadScopeTagHelperService : SmartSoftwareTagHelperService<SmartSoftwareTableHeadScopeTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetScope(context, output);
    }

    protected virtual void SetScope(TagHelperContext context, TagHelperOutput output)
    {
        switch (TagHelper.Scope)
        {
            case SmartSoftwareThScope.Default:
                return;
            case SmartSoftwareThScope.Row:
                output.Attributes.Add("scope", "row");
                return;
            case SmartSoftwareThScope.Column:
                output.Attributes.Add("scope", "col");
                return;
        }
    }
}
