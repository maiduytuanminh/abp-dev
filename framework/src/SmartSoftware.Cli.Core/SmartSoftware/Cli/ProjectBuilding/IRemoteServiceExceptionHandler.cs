using System.Net.Http;
using System.Threading.Tasks;

namespace SmartSoftware.Cli.ProjectBuilding;

public interface IRemoteServiceExceptionHandler
{
    Task EnsureSuccessfulHttpResponseAsync(HttpResponseMessage responseMessage);

    Task<string> GetSmartSoftwareRemoteServiceErrorAsync(HttpResponseMessage responseMessage);
}
