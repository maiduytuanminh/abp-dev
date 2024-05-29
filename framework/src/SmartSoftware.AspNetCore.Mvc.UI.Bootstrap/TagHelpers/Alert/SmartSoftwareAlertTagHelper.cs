using SmartSoftware.AspNetCore.Mvc.UI.Alerts;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

public class SmartSoftwareAlertTagHelper : SmartSoftwareTagHelper<SmartSoftwareAlertTagHelper, SmartSoftwareAlertTagHelperService>
{
    public AlertType AlertType { get; set; } = AlertType.Default;

    public bool? Dismissible { get; set; }

    public SmartSoftwareAlertTagHelper(SmartSoftwareAlertTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
