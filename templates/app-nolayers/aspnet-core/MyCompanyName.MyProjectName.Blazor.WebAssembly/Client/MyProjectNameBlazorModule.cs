using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyCompanyName.MyProjectName.Menus;
using MyCompanyName.MyProjectName;
using OpenIddict.Abstractions;
using SmartSoftware.Account;
using SmartSoftware.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using SmartSoftware.AspNetCore.Components.Web.Theming.Routing;
using SmartSoftware.AspNetCore.Components.WebAssembly.BasicTheme;
using SmartSoftware.Autofac.WebAssembly;
using SmartSoftware.AutoMapper;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Identity.Blazor.WebAssembly;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict;
using SmartSoftware.PermissionManagement;
using SmartSoftware.SettingManagement;
using SmartSoftware.SettingManagement.Blazor.WebAssembly;
using SmartSoftware.TenantManagement;
using SmartSoftware.TenantManagement.Blazor.WebAssembly;
using SmartSoftware.UI.Navigation;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameContractsModule),

    // SS Framework packages
    typeof(SmartSoftwareAutofacWebAssemblyModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyBasicThemeModule),

    // Account module packages
    typeof(SmartSoftwareAccountHttpApiClientModule),

    // Identity module packages
    typeof(SmartSoftwareIdentityHttpApiClientModule),
    typeof(SmartSoftwareIdentityBlazorWebAssemblyModule),
    typeof(SmartSoftwareOpenIddictDomainSharedModule),

    // Permission Management module packages
    typeof(SmartSoftwarePermissionManagementHttpApiClientModule),

    // Tenant Management module packages
    typeof(SmartSoftwareTenantManagementHttpApiClientModule),
    typeof(SmartSoftwareTenantManagementBlazorWebAssemblyModule),

    // Feature Management module packages
    typeof(SmartSoftwareFeatureManagementHttpApiClientModule),

    // Setting Management module packages
    typeof(SmartSoftwareSettingManagementHttpApiClientModule),
    typeof(SmartSoftwareSettingManagementBlazorWebAssemblyModule)
)]
public class MyProjectNameBlazorModule : SmartSoftwareModule
{
    public const string RemoteServiceName = "Default";

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
        ConfigureHttpClientProxies(context);
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareRouterOptions>(options =>
        {
            options.AppAssembly = typeof(MyProjectNameBlazorModule).Assembly;
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

    private void ConfigureHttpClientProxies(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(MyProjectNameContractsModule).Assembly,
            RemoteServiceName
        );
    }

    private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AuthServer", options.ProviderOptions);
            options.UserOptions.NameClaim = OpenIddictConstants.Claims.Name;
            options.UserOptions.RoleClaim = OpenIddictConstants.Claims.Role;

            options.ProviderOptions.DefaultScopes.Add("MyProjectName");
            options.ProviderOptions.DefaultScopes.Add("roles");
            options.ProviderOptions.DefaultScopes.Add("email");
            options.ProviderOptions.DefaultScopes.Add("phone");
        });
    }

    private static void ConfigureUI(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#ApplicationContainer");
        builder.RootComponents.Add<HeadOutlet>("head::after");
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
            options.AddMaps<MyProjectNameBlazorModule>();
        });
    }
}
