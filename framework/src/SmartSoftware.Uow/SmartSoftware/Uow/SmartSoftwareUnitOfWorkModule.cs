using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.Uow;

public class SmartSoftwareUnitOfWorkModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(UnitOfWorkInterceptorRegistrar.RegisterIfNeeded);
    }
}
