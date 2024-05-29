using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.Authentication;

namespace SmartSoftware.Http.Client.IdentityModel.MauiBlazor;

[Dependency(ReplaceServices = true)]
public class MauiBlazorSmartSoftwareAccessTokenProvider : ISmartSoftwareAccessTokenProvider, ITransientDependency
{
    public virtual Task<string?> GetTokenAsync()
    {
        return Task.FromResult(null as string);
    }
}
