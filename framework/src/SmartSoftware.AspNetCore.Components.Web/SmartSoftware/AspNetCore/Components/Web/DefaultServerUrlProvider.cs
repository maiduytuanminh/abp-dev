using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web;

public class DefaultServerUrlProvider : IServerUrlProvider, ISingletonDependency
{
    public Task<string> GetBaseUrlAsync(string? remoteServiceName = null)
    {
        return Task.FromResult("/");
    }
}
