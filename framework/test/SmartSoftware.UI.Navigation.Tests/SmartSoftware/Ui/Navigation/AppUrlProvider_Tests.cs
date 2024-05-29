using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.MultiTenancy;
using SmartSoftware.MultiTenancy.ConfigurationStore;
using SmartSoftware.Testing;
using SmartSoftware.UI.Navigation.Urls;
using Xunit;

namespace SmartSoftware.UI.Navigation;

public class AppUrlProvider_Tests : SmartSoftwareIntegratedTest<SmartSoftwareUiNavigationTestModule>
{
    private readonly IAppUrlProvider _appUrlProvider;
    private readonly ICurrentTenant _currentTenant;

    private readonly Guid _tenantAId = Guid.NewGuid();

    public AppUrlProvider_Tests()
    {
        _appUrlProvider = ServiceProvider.GetRequiredService<AppUrlProvider>();
        _currentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
    }

    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = "https://{{tenantName}}.smartsoftware.io";
            options.Applications["MVC"].Urls["PasswordReset"] = "account/reset-password";
            options.RedirectAllowedUrls.AddRange(new List<string>()
            {
                "https://wwww.smartsoftware.com",
                "https://wwww.aspnetzero.com",
                "https://{{tenantName}}.smartsoftware.io",
                "https://{{tenantId}}.smartsoftware.io"
            });

            options.Applications["BLAZOR"].RootUrl = "https://{{tenantId}}.smartsoftware.io";
            options.Applications["BLAZOR"].Urls["PasswordReset"] = "account/reset-password";
        });

        services.Configure<SmartSoftwareDefaultTenantStoreOptions>(options =>
        {
            options.Tenants = new TenantConfiguration[]
            {
                new(_tenantAId, "community")
            };
        });
    }

    [Fact]
    public async Task GetUrlAsync()
    {
        using (_currentTenant.Change(null))
        {
            var url = await _appUrlProvider.GetUrlAsync("MVC");
            url.ShouldBe("https://smartsoftware.io");

            url = await _appUrlProvider.GetUrlAsync("MVC", "PasswordReset");
            url.ShouldBe("https://smartsoftware.io/account/reset-password");
        }

        using (_currentTenant.Change(Guid.NewGuid(), "community"))
        {
            var url = await _appUrlProvider.GetUrlAsync("MVC");
            url.ShouldBe("https://community.smartsoftware.io");

            url = await _appUrlProvider.GetUrlAsync("MVC", "PasswordReset");
            url.ShouldBe("https://community.smartsoftware.io/account/reset-password");
        }

        using (_currentTenant.Change(_tenantAId))
        {
            var url = await _appUrlProvider.GetUrlAsync("BLAZOR");
            url.ShouldBe($"https://{_tenantAId}.smartsoftware.io");

            url = await _appUrlProvider.GetUrlAsync("BLAZOR", "PasswordReset");
            url.ShouldBe($"https://{_tenantAId}.smartsoftware.io/account/reset-password");
        }

        await Assert.ThrowsAsync<SmartSoftwareException>(async () =>
        {
            await _appUrlProvider.GetUrlAsync("ANGULAR");
        });
    }

    [Fact]
    public async Task GetUrlOrNullAsync()
    {
        (await _appUrlProvider.GetUrlOrNullAsync("ANGULAR")).ShouldBeNull();
    }

    [Fact]
    public async Task IsRedirectAllowedUrlAsync()
    {
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community.smartsoftware.io")).ShouldBeFalse();
        (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://wwww.smartsoftware.com")).ShouldBeTrue();

        using (_currentTenant.Change(null))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://www.smartsoftware.io")).ShouldBeFalse();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://smartsoftware.io")).ShouldBeTrue();
        }

        using (_currentTenant.Change(_tenantAId, "community"))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community.smartsoftware.io")).ShouldBeTrue();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync("https://community2.smartsoftware.io")).ShouldBeFalse();
        }

        using (_currentTenant.Change(_tenantAId))
        {
            (await _appUrlProvider.IsRedirectAllowedUrlAsync($"https://{_tenantAId}.smartsoftware.io")).ShouldBeTrue();
            (await _appUrlProvider.IsRedirectAllowedUrlAsync($"https://{Guid.NewGuid()}.smartsoftware.io")).ShouldBeFalse();
        }
    }
}
