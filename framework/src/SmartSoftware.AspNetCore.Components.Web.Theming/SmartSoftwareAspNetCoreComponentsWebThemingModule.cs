using SmartSoftware.AspNetCore.Components.Web.Security;
using SmartSoftware.BlazoriseUI;
using SmartSoftware.Modularity;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.AspNetCore.Components.Web.Theming;

[DependsOn(
    typeof(SmartSoftwareBlazoriseUIModule),
    typeof(SmartSoftwareUiNavigationModule)
    )]
public class SmartSoftwareAspNetCoreComponentsWebThemingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareDynamicLayoutComponentOptions>(options =>
        {
            options.Components.Add(typeof(SmartSoftwareAuthenticationState), null);
        });
    }
}
