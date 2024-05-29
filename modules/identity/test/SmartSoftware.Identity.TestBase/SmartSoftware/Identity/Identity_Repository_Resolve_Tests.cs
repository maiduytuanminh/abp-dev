using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Modularity;
using Xunit;

namespace SmartSoftware.Identity;

public abstract class Identity_Repository_Resolve_Tests<TStartupModule> : SmartSoftwareIdentityTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    [Fact] //Move this test to SmartSoftware.EntityFrameworkCore.Tests since it's actually testing the EF Core repository registration!
    public void Should_Resolve_Repositories()
    {
        ServiceProvider.GetService<IRepository<IdentityUser>>().ShouldNotBeNull();
        ServiceProvider.GetService<IRepository<IdentityUser, Guid>>().ShouldNotBeNull();
        ServiceProvider.GetService<IIdentityUserRepository>().ShouldNotBeNull();

        ServiceProvider.GetService<IRepository<IdentityRole>>().ShouldNotBeNull();
        ServiceProvider.GetService<IRepository<IdentityRole, Guid>>().ShouldNotBeNull();
        ServiceProvider.GetService<IIdentityRoleRepository>().ShouldNotBeNull();

        ServiceProvider.GetService<IOrganizationUnitRepository>().ShouldNotBeNull();
    }
}
