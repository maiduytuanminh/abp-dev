using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.DependencyInjection;
using SmartSoftware.DynamicProxy;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Security;
using SmartSoftware.Timing;

namespace SmartSoftware.AspNetCore.Components;

[DependsOn(
    typeof(SmartSoftwareObjectMappingModule),
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareTimingModule),
    typeof(SmartSoftwareMultiTenancyAbstractionsModule)
    )]
public class SmartSoftwareAspNetCoreComponentsModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        DynamicProxyIgnoreTypes.Add<ComponentBase>();
        context.Services.AddConventionalRegistrar(new SmartSoftwareWebAssemblyConventionalRegistrar());
    }
}
