using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Security.Claims;

[DisableConventionalRegistration]
class TestSmartSoftwareDynamicClaimsPrincipalContributor : SmartSoftwareDynamicClaimsPrincipalContributorBase
{
    private readonly List<SmartSoftwareDynamicClaim> _claims;

    public TestSmartSoftwareDynamicClaimsPrincipalContributor(List<SmartSoftwareDynamicClaim> claims)
    {
        _claims = claims;
    }

    public async override Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
    {
        var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
        Check.NotNull(identity, nameof(identity));

        await AddDynamicClaimsAsync(context, identity, _claims);
    }
}

public class SmartSoftwareDynamicClaimsPrincipalContributorBase_Tests : SmartSoftwareIntegratedTest<SmartSoftwareSecurityTestModule>
{
    private readonly TestSmartSoftwareDynamicClaimsPrincipalContributor _dynamicClaimsPrincipalContributorBase;

    private readonly SmartSoftwareDynamicClaimCacheItem _dynamicClaims;

    public SmartSoftwareDynamicClaimsPrincipalContributorBase_Tests()
    {
        _dynamicClaims = new SmartSoftwareDynamicClaimCacheItem(new List<SmartSoftwareDynamicClaim>()
        {
            new SmartSoftwareDynamicClaim("preferred_username", "test-preferred_username"),
            new SmartSoftwareDynamicClaim(ClaimTypes.GivenName, "test-given_name"),
            new SmartSoftwareDynamicClaim("family_name", "test-family_name"),
            new SmartSoftwareDynamicClaim("role", "test-role1"),
            new SmartSoftwareDynamicClaim("roles", "test-role2"),
            new SmartSoftwareDynamicClaim(ClaimTypes.Role, "test-role3"),
            new SmartSoftwareDynamicClaim("email", "test-email"),
            new SmartSoftwareDynamicClaim(SmartSoftwareClaimTypes.EmailVerified, "test-email-verified"),
            new SmartSoftwareDynamicClaim(SmartSoftwareClaimTypes.PhoneNumberVerified, null),
        });
        _dynamicClaimsPrincipalContributorBase = new TestSmartSoftwareDynamicClaimsPrincipalContributor(_dynamicClaims.Claims);
    }

    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task AddDynamicClaimsAsync()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.UserName, "test-source-userName"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.Name, "test-source-name"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.SurName, "test-source-surname"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.Role, "test-source-role1"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.Role, "test-source-role2"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.Email, "test-source-email"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.EmailVerified, "test-source-emailVerified"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.PhoneNumber, "test-source-phoneNumber"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(SmartSoftwareClaimTypes.PhoneNumberVerified, "test-source-phoneNumberVerified"));
        claimsPrincipal.Identities.First().AddClaim(new Claim("my-claim", "test-source-my-claim"));

        await _dynamicClaimsPrincipalContributorBase.ContributeAsync(new SmartSoftwareClaimsPrincipalContributorContext(claimsPrincipal, GetRequiredService<IServiceProvider>()));

        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.UserName && c.Value == "test-preferred_username");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.SurName && c.Value == "test-family_name");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.Name && c.Value == "test-given_name");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.Role && c.Value == "test-role1");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.Role && c.Value == "test-role2");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.Role && c.Value == "test-role3");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.Email && c.Value == "test-email");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.EmailVerified && c.Value == "test-email-verified");
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == SmartSoftwareClaimTypes.PhoneNumber && c.Value == "test-source-phoneNumber");
        claimsPrincipal.Identities.First().Claims.ShouldNotContain(c => c.Type == SmartSoftwareClaimTypes.PhoneNumberVerified);
        claimsPrincipal.Identities.First().Claims.ShouldContain(c => c.Type == "my-claim" && c.Value == "test-source-my-claim");
    }
}
