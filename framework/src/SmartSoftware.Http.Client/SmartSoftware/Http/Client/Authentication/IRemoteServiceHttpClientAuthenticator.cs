using System.Threading.Tasks;

namespace SmartSoftware.Http.Client.Authentication;

public interface IRemoteServiceHttpClientAuthenticator
{
    Task Authenticate(RemoteServiceHttpClientAuthenticateContext context); //TODO: Rename to AuthenticateAsync
}
