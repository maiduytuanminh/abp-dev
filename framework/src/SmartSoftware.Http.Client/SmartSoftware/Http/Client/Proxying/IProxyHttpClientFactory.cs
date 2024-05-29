using System.Net.Http;

namespace SmartSoftware.Http.Client.Proxying;

public interface IProxyHttpClientFactory
{
    HttpClient Create();

    HttpClient Create(string name);
}
