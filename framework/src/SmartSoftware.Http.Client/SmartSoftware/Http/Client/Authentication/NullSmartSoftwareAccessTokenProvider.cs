using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Http.Client.Authentication;

public class NullSmartSoftwareAccessTokenProvider : ISmartSoftwareAccessTokenProvider, ITransientDependency
{
    public Task<string?> GetTokenAsync()
    {
        return Task.FromResult(null as string);
    }
}
