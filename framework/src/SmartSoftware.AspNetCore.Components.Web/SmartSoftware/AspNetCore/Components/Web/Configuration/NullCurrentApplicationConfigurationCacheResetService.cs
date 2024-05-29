using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.Configuration;

public class NullCurrentApplicationConfigurationCacheResetService : ICurrentApplicationConfigurationCacheResetService, ISingletonDependency
{
    public Task ResetAsync()
    {
        return Task.CompletedTask;
    }
}
