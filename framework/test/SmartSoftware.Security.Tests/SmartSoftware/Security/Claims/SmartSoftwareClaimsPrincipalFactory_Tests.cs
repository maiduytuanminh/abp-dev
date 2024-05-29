using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Security.Claims;

public class SmartSoftwareClaimsPrincipalFactory_Tests : SmartSoftwareIntegratedTest<SmartSoftwareSecurityTestModule>
{
    private readonly ISmartSoftwareClaimsPrincipalFactory _ssClaimsPrincipalFactory;
    private static string TestAuthenticationType => "Identity.Application";

    public SmartSoftwareClaimsPrincipalFactory_Tests()
    {
        _ssClaimsPrincipalFactory = GetRequiredService<ISmartSoftwareClaimsPrincipalFactory>();

    }

    protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var claimsPrincipal = await _ssClaimsPrincipalFactory.CreateAsync();
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@smartsoftware.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@smartsoftware.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    [Fact]
    public async Task Create_With_Exists_ClaimsPrincipal()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TestAuthenticationType, ClaimTypes.Name, ClaimTypes.Role));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Name, "123"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Role, "admin"));

        await _ssClaimsPrincipalFactory.CreateAsync(claimsPrincipal);
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == "123");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@smartsoftware.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@smartsoftware.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    [Fact]
    public async Task DynamicCreateAsync()
    {
        var claimsPrincipal = await _ssClaimsPrincipalFactory.CreateDynamicAsync();
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "manager");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@smartsoftware.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@smartsoftware.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    [Fact]
    public async Task DynamicCreate_With_Exists_ClaimsPrincipal()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(TestAuthenticationType, ClaimTypes.Name, ClaimTypes.Role));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Name, "123"));
        claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Role, "123"));

        await _ssClaimsPrincipalFactory.CreateDynamicAsync(claimsPrincipal);
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Name && x.Value == "123");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Role && x.Value == "123");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Name && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Role && x.Value == "manager");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Email && x.Value == "admin2@smartsoftware.io");
        claimsPrincipal.Claims.ShouldNotContain(x => x.Type == ClaimTypes.Email && x.Value == "admin@smartsoftware.io");
        claimsPrincipal.Claims.ShouldContain(x => x.Type == ClaimTypes.Version && x.Value == "2.0");
    }

    class TestSmartSoftwareClaimsPrincipalContributor : ISmartSoftwareClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin@smartsoftware.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test2SmartSoftwareClaimsPrincipalContributor : ISmartSoftwareClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin2@smartsoftware.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test3SmartSoftwareClaimsPrincipalContributor : ISmartSoftwareClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Version, "2.0"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class TestSmartSoftwareDynamicClaimsPrincipalContributor : ISmartSoftwareDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin@smartsoftware.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test2SmartSoftwareDynamicClaimsPrincipalContributor : ISmartSoftwareDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Email, "admin2@smartsoftware.io"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test3SmartSoftwareDynamicClaimsPrincipalContributor : ISmartSoftwareDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Version, "2.0"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }

    class Test4SmartSoftwareDynamicClaimsPrincipalContributor : ISmartSoftwareDynamicClaimsPrincipalContributor, ITransientDependency
    {
        public Task ContributeAsync(SmartSoftwareClaimsPrincipalContributorContext context)
        {
            var claimsIdentity = context.ClaimsPrincipal.Identities.FirstOrDefault(x => x.AuthenticationType == TestAuthenticationType)
                                 ?? new ClaimsIdentity(TestAuthenticationType);

            claimsIdentity.AddOrReplace(new Claim(ClaimTypes.Name, "admin"));
            claimsIdentity.RemoveAll(ClaimTypes.Role);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "manager"));

            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);

            return Task.CompletedTask;
        }
    }
}
