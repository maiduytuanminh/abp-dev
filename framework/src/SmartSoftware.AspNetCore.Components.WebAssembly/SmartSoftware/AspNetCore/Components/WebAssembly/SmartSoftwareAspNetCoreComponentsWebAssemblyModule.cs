using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SmartSoftware.AspNetCore.Components.Server;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.AspNetCore.Components.Web.ExceptionHandling;
using SmartSoftware.AspNetCore.Components.Web.Security;
using SmartSoftware.AspNetCore.Mvc.Client;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.UI;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcClientCommonModule),
    typeof(SmartSoftwareUiModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebModule)
)]
public class SmartSoftwareAspNetCoreComponentsWebAssemblyModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var ssHostEnvironment = context.Services.GetSingletonInstance<ISmartSoftwareHostEnvironment>();
        if (ssHostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
        {
            ssHostEnvironment.EnvironmentName = context.Services.GetWebAssemblyHostEnvironment().Environment;
        }

        PreConfigure<SmartSoftwareHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((_, builder) =>
            {
                builder.AddHttpMessageHandler<SmartSoftwareBlazorClientHttpMessageHandler>();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
        context.Services
            .GetHostBuilder().Logging
            .AddProvider(new SmartSoftwareExceptionHandlingLoggerProvider(context.Services));
        
        if (!context.Services.ExecutePreConfiguredActions<SmartSoftwareAspNetCoreComponentsWebOptions>().IsBlazorWebApp)
        {
            Configure<SmartSoftwareAuthenticationOptions>(options =>
            {
                options.LoginUrl = "authentication/login";
                options.LogoutUrl = "authentication/logout";
            });
        }
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var msAuthenticationStateProvider = context.Services.FirstOrDefault(x => x.ServiceType == typeof(AuthenticationStateProvider));
        if (msAuthenticationStateProvider is {ImplementationType: not null} &&
            msAuthenticationStateProvider.ImplementationType.IsGenericType &&
            msAuthenticationStateProvider.ImplementationType.GetGenericTypeDefinition() == typeof(RemoteAuthenticationService<,,>))
        {
            var webAssemblyAuthenticationStateProviderType = typeof(WebAssemblyAuthenticationStateProvider<,,>).MakeGenericType(
                    msAuthenticationStateProvider.ImplementationType.GenericTypeArguments[0],
                    msAuthenticationStateProvider.ImplementationType.GenericTypeArguments[1],
                    msAuthenticationStateProvider.ImplementationType.GenericTypeArguments[2]);

            context.Services.Replace(ServiceDescriptor.Scoped(typeof(AuthenticationStateProvider), webAssemblyAuthenticationStateProviderType));
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<WebAssemblyCachedApplicationConfigurationClient>().InitializeAsync();
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<SmartSoftwareComponentsClaimsCache>().InitializeAsync();
        await SetCurrentLanguageAsync(context.ServiceProvider);
    }

    private async static Task SetCurrentLanguageAsync(IServiceProvider serviceProvider)
    {
        var configurationClient = serviceProvider.GetRequiredService<ICachedApplicationConfigurationClient>();
        var utilsService = serviceProvider.GetRequiredService<ISmartSoftwareUtilsService>();
        var configuration = await configurationClient.GetAsync();
        var cultureName = configuration.Localization?.CurrentCulture?.CultureName;
        if (!cultureName.IsNullOrEmpty())
        {
            var culture = new CultureInfo(cultureName!);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
        {
            await utilsService.AddClassToTagAsync("body", "rtl");
        }
    }
}
