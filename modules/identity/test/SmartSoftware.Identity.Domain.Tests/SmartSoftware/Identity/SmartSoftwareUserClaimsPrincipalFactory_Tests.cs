using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;
using Xunit;

namespace SmartSoftware.Identity;

public class SmartSoftwareUserClaimsPrincipalFactory_Tests : SmartSoftwareIdentityDomainTestBase
{
    private readonly IdentityUserManager _identityUserManager;
    private readonly SmartSoftwareUserClaimsPrincipalFactory _ssUserClaimsPrincipalFactory;
    private readonly IdentityTestData _testData;

    public SmartSoftwareUserClaimsPrincipalFactory_Tests()
    {
        _identityUserManager = GetRequiredService<IdentityUserManager>();
        _ssUserClaimsPrincipalFactory = GetRequiredService<SmartSoftwareUserClaimsPrincipalFactory>();
        _testData = GetRequiredService<IdentityTestData>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddTransient<TestSmartSoftwareClaimsPrincipalContributor>();
    }

    [Fact]
    public async Task Add_And_Replace_Claims_Test()
    {
        await UsingUowAsync(async () =>
        {
            var user = await _identityUserManager.GetByIdAsync(_testData.UserJohnId);
            user.ShouldNotBeNull();

            var claimsPrincipal = await _ssUserClaimsPrincipalFactory.CreateAsync(user);

            claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.NameIdentifier && x.Value == user.Id.ToString());
            claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == user.UserName);

            claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Uri && x.Value == "www.smartsoftware.io");

            claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == user.Email);
            claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "replaced@smartsoftware.io");
        });
    }

    class TestSmartSoftwareClaimsPrincipalContributor : ISmartSoftwareClaimsPrincipalContributor
    {
        //https://github.com/dotnet/aspnetcore/blob/v5.0.0/src/Identity/Extensions.Core/src/UserClaimsPrincipalFactory.cs#L79
        private static string IdentityAuthenticationType => "Identity.Application";

        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.First(x => x.AuthenticationType == IdentityAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Uri, "www.smartsoftware.io"));
            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "replaced@smartsoftware.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

}
