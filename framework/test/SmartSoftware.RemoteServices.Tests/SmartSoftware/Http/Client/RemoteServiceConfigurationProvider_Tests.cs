using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.MultiTenancy;
using SmartSoftware.MultiTenancy.ConfigurationStore;
using Xunit;

namespace SmartSoftware.Http.Client;

public class RemoteServiceConfigurationProvider_Tests : SmartSoftwareRemoteServicesTestBase
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IRemoteServiceConfigurationProvider _remoteServiceConfigurationProvider;
    private readonly Guid _tenantAId = Guid.NewGuid();
    
    public RemoteServiceConfigurationProvider_Tests()
    {
        _currentTenant =  GetRequiredService<ICurrentTenant>();
        _remoteServiceConfigurationProvider = GetRequiredService<IRemoteServiceConfigurationProvider>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<SmartSoftwareRemoteServiceOptions>(options =>
        {
            options.RemoteServices[RemoteServiceConfigurationDictionary.DefaultName] = new RemoteServiceConfiguration("https://smartsoftware.io");
            options.RemoteServices["Identity"] = new RemoteServiceConfiguration("https://{{tenantName}}.smartsoftware.io");
            options.RemoteServices["Permission"] = new RemoteServiceConfiguration("https://{{tenantId}}.smartsoftware.io");
            options.RemoteServices["Setting"] = new RemoteServiceConfiguration("https://{0}.smartsoftware.io");
        });
        
        services.Configure<SmartSoftwareDefaultTenantStoreOptions>(options =>
        {
            options.Tenants =
            [
                new TenantConfiguration(_tenantAId, "TenantA")
            ];
        });
    }

    [Fact]
    public async Task GetConfigurationOrDefaultAsync()
    {
        _currentTenant.Id.ShouldBeNull();
        
        var defaultConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(RemoteServiceConfigurationDictionary.DefaultName);
        defaultConfiguration.BaseUrl.ShouldBe("https://smartsoftware.io");

        var identityConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Identity");
        identityConfiguration.BaseUrl.ShouldBe("https://smartsoftware.io");

        var permissionConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Permission");
        permissionConfiguration.BaseUrl.ShouldBe("https://smartsoftware.io");
        
        var settingConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Setting");
        settingConfiguration.BaseUrl.ShouldBe("https://smartsoftware.io");

        using (_currentTenant.Change(_tenantAId, "TenantA"))
        { 
            defaultConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(RemoteServiceConfigurationDictionary.DefaultName);
            defaultConfiguration.BaseUrl.ShouldBe("https://smartsoftware.io");

            identityConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Identity");
            identityConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Name}.smartsoftware.io");

            permissionConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Permission");
            permissionConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Id}.smartsoftware.io");
            
            settingConfiguration = await _remoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Setting");
            settingConfiguration.BaseUrl.ShouldBe($"https://{_currentTenant.Name}.smartsoftware.io");
        }
    }
}