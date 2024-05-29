using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareWildcardSubdomainCorsPolicyService_Tests : SmartSoftwareIdentityServerTestBase
{
    private readonly ICorsPolicyService _corsPolicyService;

    public SmartSoftwareWildcardSubdomainCorsPolicyService_Tests()
    {
        _corsPolicyService = GetRequiredService<ICorsPolicyService>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddSmartSoftwareWildcardSubdomainCorsPolicyService();
    }

    [Fact]
    public void Should_Register_SmartSoftwareWildcardSubdomainCorsPolicyService()
    {
        _corsPolicyService.GetType().ShouldBe(typeof(SmartSoftwareWildcardSubdomainCorsPolicyService));
    }

    [Fact]
    public async Task IsOriginAllowedAsync()
    {
        (await _corsPolicyService.IsOriginAllowedAsync("https://client1-origin.com")).ShouldBeTrue();
        (await _corsPolicyService.IsOriginAllowedAsync("https://client2-origin.com")).ShouldBeFalse();

        (await _corsPolicyService.IsOriginAllowedAsync("https://smartsoftware.io")).ShouldBeTrue();
        (await _corsPolicyService.IsOriginAllowedAsync("https://t1.smartsoftware.io")).ShouldBeTrue();
        (await _corsPolicyService.IsOriginAllowedAsync("https://t1.ng.smartsoftware.io")).ShouldBeTrue();

        (await _corsPolicyService.IsOriginAllowedAsync("https://t1.smartsoftware.io.mydomain.com")).ShouldBeFalse();
    }
}
