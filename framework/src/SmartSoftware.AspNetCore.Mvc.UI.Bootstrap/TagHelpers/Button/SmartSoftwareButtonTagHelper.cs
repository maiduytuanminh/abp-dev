using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

[HtmlTargetElement("ss-button", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SmartSoftwareButtonTagHelper : SmartSoftwareTagHelper<SmartSoftwareButtonTagHelper, SmartSoftwareButtonTagHelperService>, IButtonTagHelperBase
{
    public SmartSoftwareButtonType ButtonType { get; set; } = SmartSoftwareButtonType.Default;

    public SmartSoftwareButtonSize Size { get; set; } = SmartSoftwareButtonSize.Default;

    public string? BusyText { get; set; }

    public string? Text { get; set; }

    public string? Icon { get; set; }

    public bool? Disabled { get; set; }

    public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

    public bool BusyTextIsHtml { get; set; }

    public SmartSoftwareButtonTagHelper(SmartSoftwareButtonTagHelperService service)
        : base(service)
    {

    }
}

