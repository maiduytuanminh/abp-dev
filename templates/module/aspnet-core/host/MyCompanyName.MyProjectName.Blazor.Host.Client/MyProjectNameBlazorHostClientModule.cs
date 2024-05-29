﻿using System;
using System.Net.Http;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.MyProjectName.Blazor.WebAssembly;
using SmartSoftware.Account;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AspNetCore.Components.WebAssembly.BasicTheme;
using SmartSoftware.Autofac.WebAssembly;
using SmartSoftware.AutoMapper;
using SmartSoftware.Identity.Blazor.WebAssembly;
using SmartSoftware.Modularity;
using SmartSoftware.SettingManagement.Blazor.WebAssembly;
using SmartSoftware.TenantManagement.Blazor.WebAssembly;
using SmartSoftware.UI.Navigation;

namespace MyCompanyName.MyProjectName.Blazor.Host.Client;

[DependsOn(
    typeof(SmartSoftwareAutofacWebAssemblyModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyBasicThemeModule),
    typeof(SmartSoftwareAccountApplicationContractsModule),
    typeof(SmartSoftwareIdentityBlazorWebAssemblyModule),
    typeof(SmartSoftwareTenantManagementBlazorWebAssemblyModule),
    typeof(SmartSoftwareSettingManagementBlazorWebAssemblyModule),
    typeof(MyProjectNameBlazorWebAssemblyModule)
)]
public class MyProjectNameBlazorHostClientModule : SmartSoftwareModule
{
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
            options.AppAssembly = typeof(MyProjectNameBlazorHostClientModule).Assembly;
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MyProjectNameHostMenuContributor(context.Services.GetConfiguration()));
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
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.ProviderOptions.DefaultScopes.Add("MyProjectName");
        });
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
            options.AddMaps<MyProjectNameBlazorHostClientModule>();
        });
    }
}
