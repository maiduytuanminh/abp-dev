using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Castle.DynamicProxy;
using SmartSoftware.Modularity;

namespace SmartSoftware.Castle;

public class SmartSoftwareCastleCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(SmartSoftwareAsyncDeterminationInterceptor<>));
    }
}
