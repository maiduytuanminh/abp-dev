using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.AspNetCore.Components.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Server;
using SmartSoftware.Modularity;
using SmartSoftware.UI;

namespace SmartSoftware.AspNetCore.Components.Web;

[DependsOn(
    typeof(SmartSoftwareUiModule),
    typeof(SmartSoftwareAspNetCoreComponentsModule)
    )]
public class SmartSoftwareAspNetCoreComponentsWebModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Replace(ServiceDescriptor.Transient<IComponentActivator, ServiceProviderComponentActivator>());

        var preActions = context.Services.GetPreConfigureActions<SmartSoftwareAspNetCoreComponentsWebOptions>();
        Configure<SmartSoftwareAspNetCoreComponentsWebOptions>(options =>
        {
            preActions.Configure(options);
        });
    }
}
