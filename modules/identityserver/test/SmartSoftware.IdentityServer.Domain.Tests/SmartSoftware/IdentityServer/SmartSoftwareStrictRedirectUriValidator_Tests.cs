using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareStrictRedirectUriValidator_Tests : SmartSoftwareIdentityServerTestBase
{
    private readonly IRedirectUriValidator _ssStrictRedirectUriValidator;

    private readonly Client _testClient = new Client
    {
        RedirectUris = new List<string>
            {
                "https://{0}.api.smartsoftware.io:8080/signin-oidc",
                "http://{0}.ng.smartsoftware.io/index.html"
            },
        PostLogoutRedirectUris = new List<string>
            {
                "https://{0}.api.smartsoftware.io:8080/signin-oidc",
                "http://{0}.ng.smartsoftware.io/index.html"
            }
    };

    public SmartSoftwareStrictRedirectUriValidator_Tests()
    {
        _ssStrictRedirectUriValidator = GetRequiredService<IRedirectUriValidator>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddSmartSoftwareStrictRedirectUriValidator();
    }

    [Fact]
    public void Should_Register_SmartSoftwareStrictRedirectUriValidator()
    {
        _ssStrictRedirectUriValidator.GetType().ShouldBe(typeof(SmartSoftwareStrictRedirectUriValidator));
    }

    [Fact]
    public async Task IsRedirectUriValidAsync()
    {
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("https://t1.api.smartsoftware.io:8080/signin-oidc", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("https://api.smartsoftware.io:8080/signin-oidc", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("http://t2.ng.smartsoftware.io/index.html", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("http://ng.smartsoftware.io/index.html", _testClient)).ShouldBeTrue();

        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("https://api.ss:8080/", _testClient)).ShouldBeFalse();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("http://ng.smartsoftware.io", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("https://api.t1.ss:8080/", _testClient)).ShouldBeFalse();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("http://ng.t1.smartsoftware.io", _testClient)).ShouldBeFalse();
        (await _ssStrictRedirectUriValidator.IsRedirectUriValidAsync("http://t1.ng.smartsoftware.io/index.html.mydomain.com", _testClient)).ShouldBeFalse();
    }

    [Fact]
    public async Task IsPostLogoutRedirectUriValidAsync()
    {
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("https://t1.api.smartsoftware.io:8080/signin-oidc", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("https://api.smartsoftware.io:8080/signin-oidc", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("http://t2.ng.smartsoftware.io/index.html", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("http://ng.smartsoftware.io/index.html", _testClient)).ShouldBeTrue();

        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("https://api.ss:8080/", _testClient)).ShouldBeFalse();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("http://ng.smartsoftware.io", _testClient)).ShouldBeTrue();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("https://api.t1.ss:8080/", _testClient)).ShouldBeFalse();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("http://ng.t1.smartsoftware.io", _testClient)).ShouldBeFalse();
        (await _ssStrictRedirectUriValidator.IsPostLogoutRedirectUriValidAsync("http://t1.ng.smartsoftware.io/index.html.mydomain.com", _testClient)).ShouldBeFalse();
    }
}
