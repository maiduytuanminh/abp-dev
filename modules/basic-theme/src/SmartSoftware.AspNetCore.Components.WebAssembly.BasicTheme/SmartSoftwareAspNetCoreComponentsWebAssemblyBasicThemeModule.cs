using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Server;
using SmartSoftware.AspNetCore.Components.Web.BasicTheme;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;
using SmartSoftware.AspNetCore.Components.WebAssembly.Theming;
using SmartSoftware.Http.Client.IdentityModel.WebAssembly;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.WebAssembly.BasicTheme;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebBasicThemeModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(SmartSoftwareHttpClientIdentityModelWebAssemblyModule)
    )]
public class SmartSoftwareAspNetCoreComponentsWebAssemblyBasicThemeModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyBasicThemeModule).Assembly);
        });

        Configure<SmartSoftwareToolbarOptions>(options =>
        {
            options.Contributors.Add(new BasicThemeToolbarContributor());
        });

        if (context.Services.ExecutePreConfiguredActions<SmartSoftwareAspNetCoreComponentsWebOptions>().IsBlazorWebApp)
        {
            Configure<AuthenticationOptions>(options =>
            {
                options.LoginUrl = "Account/Login";
                options.LogoutUrl = "Account/Logout";
            });
        }
    }
}
