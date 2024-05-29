using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using Xunit;

namespace SmartSoftware.Identity.AspNetCore;

public class SmartSoftwareSignInManager_Tests : SmartSoftwareIdentityAspNetCoreTestBase
{
    [Fact]
    public void Should_Resolve_SmartSoftwareSignInManager()
    {
        var signInManager = GetRequiredService<SignInManager<IdentityUser>>();
        signInManager.ShouldBeOfType<SmartSoftwareSignInManager>();
    }

    [Fact]
    public async Task Should_SignIn_With_Correct_Credentials()
    {
        var result = await GetResponseAsStringAsync(
            "api/signin-test/password?userName=admin&password=1q2w3E*"
        );

        result.ShouldBe("Succeeded");
    }

    [Fact]
    public async Task Should_Not_SignIn_With_Wrong_Credentials()
    {
        var result = await GetResponseAsStringAsync(
            "api/signin-test/password?userName=admin&password=WRONG_PASSWORD"
        );

        result.ShouldBe("Failed");
    }

    [Fact]
    public async Task Should_Not_SignIn_If_User_Not_Active()
    {
        var result = await GetResponseAsStringAsync(
            "api/signin-test/password?userName=bob&password=1q2w3E*"
        );

        result.ShouldBe("NotAllowed");
    }
}
