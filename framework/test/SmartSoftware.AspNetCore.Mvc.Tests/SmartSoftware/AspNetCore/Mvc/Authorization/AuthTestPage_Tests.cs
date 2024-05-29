using System.Security.Claims;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Autofac;
using SmartSoftware.MemoryDb;
using SmartSoftware.Modularity;
using SmartSoftware.Security.Claims;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Authorization;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareMemoryDbTestModule),
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAutofacModule)
)]
public class AuthTestPage_Tests : AspNetCoreMvcTestBase
{
    private readonly FakeUserClaims _fakeRequiredService;

    public AuthTestPage_Tests()
    {
        _fakeRequiredService = GetRequiredService<FakeUserClaims>();
    }

    [Fact]
    public async Task Should_Call_Simple_Authorized_Method_With_Authenticated_User()
    {
        _fakeRequiredService.Claims.AddRange(new[]
        {
                new Claim(SmartSoftwareClaimTypes.UserId, AuthTestController.FakeUserId.ToString())
            });

        var result = await GetResponseAsStringAsync("/Authorization/AuthTestPage");
        result.ShouldBe("OK");
    }
}
