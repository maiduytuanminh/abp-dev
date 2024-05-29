using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Alerts;

public class AlertManager : IAlertManager, IScopedDependency
{
    public AlertList Alerts { get; }

    public AlertManager()
    {
        Alerts = new AlertList();
    }
}
