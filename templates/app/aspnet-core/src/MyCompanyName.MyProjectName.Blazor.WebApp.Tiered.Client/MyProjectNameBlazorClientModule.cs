using System;
using System.Net.Http;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyCompanyName.MyProjectName.Blazor.WebApp.Tiered.Client.Menus;
using SmartSoftware.AspNetCore.Components.Server;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AspNetCore.Components.WebAssembly.LeptonXLiteTheme;
using SmartSoftware.Autofac.WebAssembly;
using SmartSoftware.AutoMapper;
using SmartSoftware.Identity.Blazor.WebAssembly;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Blazor.WebAssembly;
using SmartSoftware.TenantManagement.Blazor.WebAssembly;
using SmartSoftware.UI.Navigation;

namespace MyCompanyName.MyProjectName.Blazor.WebApp.Tiered.Client;

[DependsOn(
    typeof(SmartSoftwareAutofacWebAssemblyModule),
    typeof(MyProjectNameHttpApiClientModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyLeptonXLiteThemeModule),
    typeof(SmartSoftwareIdentityBlazorWebAssemblyModule),
    typeof(SmartSoftwareTenantManagementBlazorWebAssemblyModule),
    typeof(SmartSoftwareSettingManagementBlazorWebAssemblyModule)
)]
public class MyProjectNameBlazorClientModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareAspNetCoreComponentsWebOptions>(options =>
        {
            options.IsBlazorWebApp = true;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AppAssembly = typeof(MyProjectNameBlazorClientModule).Assembly;
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MyProjectNameMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        //TODO: Remove SignOutSessionStateManager in new version.
        builder.Services.TryAddScoped<SignOutSessionStateManager>();
        builder.Services.AddBlazorWebAppTieredServices();
    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<MyProjectNameBlazorClientModule>();
        });
    }
}
