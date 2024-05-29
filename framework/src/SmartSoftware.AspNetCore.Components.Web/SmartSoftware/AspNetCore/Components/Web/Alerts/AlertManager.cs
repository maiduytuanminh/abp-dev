using SmartSoftware.AspNetCore.Components.Alerts;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.Alerts;

public class AlertManager : IAlertManager, IScopedDependency
{
    public AlertList Alerts { get; }

    public AlertManager()
    {
        Alerts = new AlertList();
    }
}
