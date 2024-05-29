using SmartSoftware.AspNetCore.Components.Alerts;

namespace SmartSoftware.BlazoriseUI.Components;

internal class AlertWrapper
{
    public AlertMessage AlertMessage { get; set; } = default!;
    public bool IsVisible { get; set; }
}
