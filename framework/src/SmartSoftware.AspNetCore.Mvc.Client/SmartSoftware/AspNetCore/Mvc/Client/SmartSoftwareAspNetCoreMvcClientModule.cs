using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.EventBus;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.Client;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcClientCommonModule),
    typeof(SmartSoftwareEventBusModule)
    )]
public class SmartSoftwareAspNetCoreMvcClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var ssHostEnvironment = context.Services.GetSmartSoftwareHostEnvironment();
        if (ssHostEnvironment.IsDevelopment())
        {
            Configure<SmartSoftwareAspNetCoreMvcClientCacheOptions>(options =>
            {
                options.ApplicationConfigurationDtoCacheAbsoluteExpiration = TimeSpan.FromSeconds(5);
            });
        }
    }
}
