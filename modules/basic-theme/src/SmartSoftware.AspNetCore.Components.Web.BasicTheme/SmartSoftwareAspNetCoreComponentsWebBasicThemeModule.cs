using SmartSoftware.AspNetCore.Components.Web.Theming;
using SmartSoftware.AspNetCore.Components.Web.Theming.Theming;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.Web.BasicTheme;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebThemingModule)
)]
public class SmartSoftwareAspNetCoreComponentsWebBasicThemeModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareThemingOptions>(options =>
        {
            options.Themes.Add<BasicTheme>();

            if (options.DefaultThemeName == null)
            {
                options.DefaultThemeName = BasicTheme.Name;
            }
        });
    }
}