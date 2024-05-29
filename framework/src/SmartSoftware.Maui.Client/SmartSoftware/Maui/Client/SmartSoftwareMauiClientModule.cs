using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc.Client;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.Maui.Client;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcClientCommonModule)
)]
public class SmartSoftwareMauiClientModule : SmartSoftwareModule
{
    public async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<MauiCachedApplicationConfigurationClient>().InitializeAsync();
    }
}
