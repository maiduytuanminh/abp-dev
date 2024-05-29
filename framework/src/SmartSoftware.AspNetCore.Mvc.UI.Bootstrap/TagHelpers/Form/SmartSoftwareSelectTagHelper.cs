﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[OutputElementHint("select")]
public class SmartSoftwareSelectTagHelper : SmartSoftwareTagHelper<SmartSoftwareSelectTagHelper, SmartSoftwareSelectTagHelperService>
{
    public ModelExpression AspFor { get; set; } = default!;

    public string? Label { get; set; }

    public bool SuppressLabel { get; set; }

    public IEnumerable<SelectListItem>? AspItems { get; set; }

    public SmartSoftwareFormControlSize Size { get; set; } = SmartSoftwareFormControlSize.Default;

    [HtmlAttributeName("info")]
    public string? InfoText { get; set; }

    [HtmlAttributeName("required-symbol")]
    public bool DisplayRequiredSymbol { get; set; } = true;

    public string? AutocompleteApiUrl { get; set; }

    public string? AutocompleteItemsPropertyName { get; set; }

    public string? AutocompleteDisplayPropertyName { get; set; }

    public string? AutocompleteValuePropertyName { get; set; }

    public string? AutocompleteFilterParamName { get; set; }

    public string? AutocompleteSelectedItemName { get; set; }

    public string? AutocompleteSelectedItemValue { get; set; }

    public string? AutocompleteParentSelector { get; set; }

    public string? AllowClear { get; set; }

    public string? Placeholder { get; set; }

    [HtmlAttributeName("floating-label")]

    public bool FloatingLabel { get; set; }

    public bool AddMarginBottomClass  { get; set; } = true;

    public SmartSoftwareSelectTagHelper(SmartSoftwareSelectTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
