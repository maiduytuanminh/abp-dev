using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict.Applications;
using Xunit;

namespace SmartSoftware.OpenIddict;

public abstract class OpenIddictApplicationRepository_Tests<TStartupModule> : OpenIddictTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    private readonly IOpenIddictApplicationRepository _applicationRepository;
    private readonly SmartSoftwareOpenIddictTestData _testData;
    
    protected OpenIddictApplicationRepository_Tests()
    {
        _applicationRepository = GetRequiredService<IOpenIddictApplicationRepository>();
        _testData = GetRequiredService<SmartSoftwareOpenIddictTestData>();
    }

    [Fact]
    public async Task GetListAsync()
    {
        (await _applicationRepository.GetListAsync()).Count.ShouldBe(2);
        (await _applicationRepository.GetListAsync("id", 1, int.MaxValue)).Count.ShouldBe(1);
        (await _applicationRepository.GetListAsync("id", 0, int.MaxValue, filter: _testData.App1ClientId)).Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetCountAsync()
    {
        (await _applicationRepository.GetCountAsync()).ShouldBe(2);
        (await _applicationRepository.GetCountAsync(filter: _testData.App1ClientId)).ShouldBe(1);
    }

    [Fact]
    public async Task FindByClientIdAsync()
    {
        var application = await _applicationRepository.FindByClientIdAsync(_testData.App1ClientId);

        application.ShouldNotBeNull();
        application.ClientId.ShouldBe(_testData.App1ClientId);
    }

    [Fact]
    public async Task FindByPostLogoutRedirectUriAsync()
    {
        var applications = await _applicationRepository.FindByPostLogoutRedirectUriAsync("https://smartsoftware.io");
        applications.Count.ShouldBe(2);
    }

    [Fact]
    public async Task FindByRedirectUriAsync()
    {
        var applications = await _applicationRepository.FindByRedirectUriAsync("https://smartsoftware.io");
        applications.Count.ShouldBe(2);
    }

    [Fact]
    public async Task ListAsync()
    {
         (await _applicationRepository.ListAsync(int.MaxValue , 0)).Count.ShouldBe(2);
         (await _applicationRepository.ListAsync(int.MaxValue , 2)).Count.ShouldBe(0);
    }
}