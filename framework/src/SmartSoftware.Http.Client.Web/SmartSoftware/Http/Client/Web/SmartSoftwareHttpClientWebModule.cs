using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.Conventions;
using SmartSoftware.Http.Client.Web.Conventions;
using SmartSoftware.Modularity;

namespace SmartSoftware.Http.Client.Web;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareHttpClientModule)
    )]
public class SmartSoftwareHttpClientWebModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Replace(ServiceDescriptor.Transient<ISmartSoftwareServiceConvention, SmartSoftwareHttpClientProxyServiceConvention>());
        context.Services.AddTransient<SmartSoftwareHttpClientProxyServiceConvention>();

        var partManager = context.Services.GetSingletonInstance<ApplicationPartManager>();
        partManager.FeatureProviders.Add(new SmartSoftwareHttpClientProxyControllerFeatureProvider());
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var partManager = context.ServiceProvider.GetRequiredService<ApplicationPartManager>();
        foreach (var moduleAssembly in context
            .ServiceProvider
            .GetRequiredService<IModuleContainer>()
            .Modules
            .SelectMany(m => m.AllAssemblies)
            .Where(a => a.GetTypes().Any(SmartSoftwareHttpClientProxyHelper.IsClientProxyService))
            .Distinct())
        {
            partManager.ApplicationParts.AddIfNotContains(moduleAssembly);
        }
    }
}
