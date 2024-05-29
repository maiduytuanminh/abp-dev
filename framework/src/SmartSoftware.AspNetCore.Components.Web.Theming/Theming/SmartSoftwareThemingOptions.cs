namespace SmartSoftware.AspNetCore.Components.Web.Theming.Theming;

public class SmartSoftwareThemingOptions
{
    public ThemeDictionary Themes { get; }

    public string? DefaultThemeName { get; set; }

    public SmartSoftwareThemingOptions()
    {
        Themes = new ThemeDictionary();
    }
}
