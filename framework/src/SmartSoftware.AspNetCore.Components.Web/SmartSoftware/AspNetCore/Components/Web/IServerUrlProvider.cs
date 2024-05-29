using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web;

public interface IServerUrlProvider
{
    Task<string> GetBaseUrlAsync(string? remoteServiceName = null);
}
