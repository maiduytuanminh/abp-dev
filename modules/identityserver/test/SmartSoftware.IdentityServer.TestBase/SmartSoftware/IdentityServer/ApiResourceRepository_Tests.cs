using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.IdentityServer;

public abstract class ApiResourceRepository_Tests<TStartupModule> : SmartSoftwareIdentityServerTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected IApiResourceRepository apiResourceRepository { get; }

    public ApiResourceRepository_Tests()
    {
        apiResourceRepository = ServiceProvider.GetRequiredService<IApiResourceRepository>();
    }

    [Fact]
    public async Task FindByNormalizedNameAsync()
    {
        (await apiResourceRepository.FindByNameAsync(new[] { "NewApiResource2" })).ShouldNotBeNull();
    }

    [Fact]
    public async Task GetListByScopesAsync()
    {
        (await apiResourceRepository.GetListByScopesAsync(new[] { "NewApiResource2", "NewApiResource3" })).Count.ShouldBe(2);
    }
}
