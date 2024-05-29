namespace SmartSoftware.AspNetCore.Mvc.UI.Widgets;

public class SmartSoftwareWidgetOptions
{
    public WidgetDefinitionCollection Widgets { get; }

    public SmartSoftwareWidgetOptions()
    {
        Widgets = new WidgetDefinitionCollection();
    }
}
