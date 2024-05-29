using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareClientConfigurationValidator_Tests : SmartSoftwareIdentityServerTestBase
{
    private readonly IClientConfigurationValidator _ssClientConfigurationValidator;

    private readonly Client _testClient = new Client
    {
        AllowedGrantTypes = GrantTypes.Code,

        ClientSecrets = new List<IdentityServer4.Models.Secret>()
            {
                new IdentityServer4.Models.Secret("1q2w3e*")
            },

        RedirectUris = new List<string>
            {
                "https://{0}.api.smartsoftware.io:8080",
                "http://{0}.ng.smartsoftware.io",
                "http://ng.smartsoftware.io"
            }
    };

    public SmartSoftwareClientConfigurationValidator_Tests()
    {
        _ssClientConfigurationValidator = GetRequiredService<IClientConfigurationValidator>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddSmartSoftwareClientConfigurationValidator();
    }

    [Fact]
    public async Task ValidateAsync()
    {
        var context = new ClientConfigurationValidationContext(_testClient);

        await _ssClientConfigurationValidator.ValidateAsync(context);

        context.IsValid.ShouldBeTrue();
    }
}
