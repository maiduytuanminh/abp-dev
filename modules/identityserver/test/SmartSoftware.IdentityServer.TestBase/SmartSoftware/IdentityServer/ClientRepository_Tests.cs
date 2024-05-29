using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.IdentityServer;

public abstract class ClientRepository_Tests<TStartupModule> : SmartSoftwareIdentityServerTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected IClientRepository clientRepository { get; }

    protected ClientRepository_Tests()
    {
        clientRepository = ServiceProvider.GetRequiredService<IClientRepository>();
    }

    [Fact]
    public async Task FindByClientIdAsync()
    {
        (await clientRepository.FindByClientIdAsync("ClientId2")).ShouldNotBeNull();
    }

    [Fact]
    public async Task GetAllDistinctAllowedCorsOriginsAsync()
    {
        var origins = await clientRepository.GetAllDistinctAllowedCorsOriginsAsync();
        origins.Any().ShouldBeTrue();
    }
}
