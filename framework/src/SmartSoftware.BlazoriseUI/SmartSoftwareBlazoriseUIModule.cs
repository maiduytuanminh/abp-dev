using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.Application;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.Authorization;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlazoriseUI;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreComponentsWebModule),
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareAuthorizationModule)
)]
public class SmartSoftwareBlazoriseUIModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureBlazorise(context);
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services.AddBlazorise(options =>
        {
            options.Debounce = true;
            options.DebounceInterval = 800;
        });

        context.Services.Replace(ServiceDescriptor.Scoped<IComponentActivator, ComponentActivator>());
        context.Services.AddSingleton(typeof(SmartSoftwareBlazorMessageLocalizerHelper<>));
    }
}