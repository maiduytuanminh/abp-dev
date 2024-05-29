using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Http.Client.Authentication;

[Dependency(TryRegister = true)]
public class NullRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency
{
    public Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        return Task.CompletedTask;
    }
}
