using System.Collections.Generic;
using System.Security.Claims;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace MyCompanyName.MyProjectName.Security;

[Dependency(ReplaceServices = true)]
public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
{
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return GetPrincipal();
    }

    private ClaimsPrincipal GetPrincipal()
    {
        return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(SmartSoftwareClaimTypes.UserId, "2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
            new Claim(SmartSoftwareClaimTypes.UserName, "admin"),
            new Claim(SmartSoftwareClaimTypes.Email, "admin@smartsoftware.io")
        }));
    }
}
