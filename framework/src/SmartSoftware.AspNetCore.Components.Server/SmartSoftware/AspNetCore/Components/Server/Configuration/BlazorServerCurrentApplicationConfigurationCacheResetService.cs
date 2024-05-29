using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Components.Web.Configuration;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus.Local;

namespace SmartSoftware.AspNetCore.Components.Server.Configuration;

[Dependency(ReplaceServices = true)]
public class BlazorServerCurrentApplicationConfigurationCacheResetService :
    ICurrentApplicationConfigurationCacheResetService,
    ITransientDependency
{
    private readonly ILocalEventBus _localEventBus;

    public BlazorServerCurrentApplicationConfigurationCacheResetService(
        ILocalEventBus localEventBus)
    {
        _localEventBus = localEventBus;
    }

    public async Task ResetAsync()
    {
        await _localEventBus.PublishAsync(
            new CurrentApplicationConfigurationCacheResetEventData()
        );
    }
}
