using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Shouldly;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.Proxying;
using Xunit;

namespace SmartSoftware.Http.DynamicProxying;

public class RegularTestControllerClientProxy_SmartSoftwareRemoteCallException_Tests : SmartSoftwareHttpClientTestBase
{
    private readonly IRegularTestController _controller;

    public RegularTestControllerClientProxy_SmartSoftwareRemoteCallException_Tests()
    {
        _controller = ServiceProvider.GetRequiredService<IRegularTestController>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton<IProxyHttpClientFactory, TestProxyHttpClientFactory>());
    }

    [Fact]
    public async Task SmartSoftwareRemoteCallException_On_SendAsync_Test()
    {
        var exception = await Assert.ThrowsAsync<SmartSoftwareRemoteCallException>(async () => await _controller.AbortRequestAsync(default));
        exception.Message.ShouldContain("An error occurred during the SS remote HTTP request.");
    }

    class TestProxyHttpClientFactory : IProxyHttpClientFactory
    {
        private readonly ITestServerAccessor _testServerAccessor;

        private int _count;

        public TestProxyHttpClientFactory(ITestServerAccessor testServerAccessor)
        {
            _testServerAccessor = testServerAccessor;
        }

        public HttpClient Create(string name) => Create();

        public HttpClient Create()
        {
            if (_count++ > 0)
            {
                //Will get an error on the SendAsync method.
                return new HttpClient();
            }

            // for DynamicHttpProxyInterceptor.GetActionApiDescriptionModel
            return _testServerAccessor.Server.CreateClient();
        }
    }
}
