using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Shouldly;
using SmartSoftware.Identity.Localization;
using Xunit;

namespace SmartSoftware.Identity.AspNetCore;

public class SmartSoftwareIdentityUserValidator_Tests : SmartSoftwareIdentityAspNetCoreTestBase
{
    private readonly IdentityUserManager _identityUserManager;
    private readonly IStringLocalizer<IdentityResource> Localizer;

    public SmartSoftwareIdentityUserValidator_Tests()
    {
        _identityUserManager = GetRequiredService<IdentityUserManager>();
        Localizer = GetRequiredService<IStringLocalizer<IdentityResource>>();
    }

    [Fact]
    public async Task Can_Not_Use_Another_Users_Email_As_Your_Username_Test()
    {
        var user1 = new IdentityUser(Guid.NewGuid(), "user1", "user1@smartsoftware.com");
        var identityResult = await _identityUserManager.CreateAsync(user1);
        identityResult.Succeeded.ShouldBeTrue();

        var user2 = new IdentityUser(Guid.NewGuid(), "user1@smartsoftware.com", "user2@smartsoftware.com");
        identityResult = await _identityUserManager.CreateAsync(user2);
        identityResult.Succeeded.ShouldBeFalse();
        identityResult.Errors.Count().ShouldBe(1);
        identityResult.Errors.First().Code.ShouldBe("InvalidUserName");
        identityResult.Errors.First().Description.ShouldBe(Localizer["InvalidUserName", "user1@smartsoftware.com"]);
    }
}
