using Shouldly;
using SmartSoftware.DynamicProxy;
using SmartSoftware.Http.Client.Authentication;
using SmartSoftware.Http.Client.IdentityModel.Web.Tests;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Http.Client.IdentityModel.Web;

public class HttpContextIdentityModelRemoteServiceHttpClientAuthenticator_Tests : SmartSoftwareIntegratedTest<SmartSoftwareHttpClientIdentityModelWebTestModule>
{
    private readonly IRemoteServiceHttpClientAuthenticator _remoteServiceHttpClientAuthenticator;

    public HttpContextIdentityModelRemoteServiceHttpClientAuthenticator_Tests()
    {
        _remoteServiceHttpClientAuthenticator = GetRequiredService<IRemoteServiceHttpClientAuthenticator>();
    }

    [Fact]
    public void Implementation_Should_Be_Type_Of_HttpContextIdentityModelRemoteServiceHttpClientAuthenticator()
    {
        ProxyHelper.UnProxy(_remoteServiceHttpClientAuthenticator)
            .ShouldBeOfType(typeof(HttpContextIdentityModelRemoteServiceHttpClientAuthenticator));
    }
}
