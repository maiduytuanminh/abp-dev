using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Components.Server.BasicTheme.Themes.Basic;
using SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;

namespace SmartSoftware.AspNetCore.Components.Server.BasicTheme;

public class BasicThemeToolbarContributor : IToolbarContributor
{
    public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name == StandardToolbars.Main)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
        }

        return Task.CompletedTask;
    }
}
