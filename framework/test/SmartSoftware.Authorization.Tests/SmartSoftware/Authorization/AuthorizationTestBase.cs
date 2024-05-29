using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using SmartSoftware.Security.Claims;
using SmartSoftware.Testing;

namespace SmartSoftware.Authorization;

public class AuthorizationTestBase : SmartSoftwareIntegratedTest<SmartSoftwareAuthorizationTestModule>
{
    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        var claims = new List<Claim>() {
                                new Claim(SmartSoftwareClaimTypes.UserName, "Douglas"),
                                new Claim(SmartSoftwareClaimTypes.UserId, "1fcf46b2-28c3-48d0-8bac-fa53268a2775"),
                                new Claim(SmartSoftwareClaimTypes.Role, "MyRole")
                            };

        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var principalAccessor = Substitute.For<ICurrentPrincipalAccessor>();
        principalAccessor.Principal.Returns(ci => claimsPrincipal);
        Thread.CurrentPrincipal = claimsPrincipal;
    }
}
