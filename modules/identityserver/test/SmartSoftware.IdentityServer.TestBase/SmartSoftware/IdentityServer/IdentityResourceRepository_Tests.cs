using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.IdentityServer;

public abstract class IdentityResourceRepository_Tests<TStartupModule> : SmartSoftwareIdentityServerTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected IIdentityResourceRepository identityResourceRepository;

    public IdentityResourceRepository_Tests()
    {
        identityResourceRepository = ServiceProvider.GetRequiredService<IIdentityResourceRepository>();
    }

    [Fact]
    public async Task GetListByScopesAsync()
    {
        (await identityResourceRepository.GetListByScopeNameAsync(new[] { "", "NewIdentityResource2" })).Count.ShouldBe(1);
    }
}
