using System.Linq;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theming;

public class DefaultThemeSelector : IThemeSelector, ITransientDependency
{
    protected SmartSoftwareThemingOptions Options { get; }

    public DefaultThemeSelector(IOptions<SmartSoftwareThemingOptions> options)
    {
        Options = options.Value;
    }

    public virtual ThemeInfo GetCurrentThemeInfo()
    {
        if (!Options.Themes.Any())
        {
            throw new SmartSoftwareException($"No theme registered! Use {nameof(SmartSoftwareThemingOptions)} to register themes.");
        }

        if (Options.DefaultThemeName == null)
        {
            return Options.Themes.Values.First();
        }

        var themeInfo = Options.Themes.Values.FirstOrDefault(t => t.Name == Options.DefaultThemeName);
        if (themeInfo == null)
        {
            throw new SmartSoftwareException("Default theme is configured but it's not found in the registered themes: " + Options.DefaultThemeName);
        }

        return themeInfo;
    }
}
