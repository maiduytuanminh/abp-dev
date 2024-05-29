using System.Net.Http;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.DynamicProxying;
using SmartSoftware.Http.Client.Proxying;

namespace SmartSoftware.AspNetCore.TestBase.DynamicProxying;

[Dependency(ReplaceServices = true)]
public class AspNetCoreTestProxyHttpClientFactory : IProxyHttpClientFactory, ITransientDependency
{
    private readonly ITestServerAccessor _testServerAccessor;

    public AspNetCoreTestProxyHttpClientFactory(
        ITestServerAccessor testServerAccessor)
    {
        _testServerAccessor = testServerAccessor;
    }

    public HttpClient Create()
    {
        return _testServerAccessor.Server.CreateClient();
    }

    public HttpClient Create(string name)
    {
        return Create();
    }
}
