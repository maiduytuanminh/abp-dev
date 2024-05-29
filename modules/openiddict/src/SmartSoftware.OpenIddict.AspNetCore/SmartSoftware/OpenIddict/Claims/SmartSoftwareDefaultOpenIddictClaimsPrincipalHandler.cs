using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using OpenIddict.Abstractions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareDefaultOpenIddictClaimsPrincipalHandler : ISmartSoftwareOpenIddictClaimsPrincipalHandler, ITransientDependency
{
    public virtual Task HandleAsync(SmartSoftwareOpenIddictClaimsPrincipalHandlerContext context)
    {
        var securityStampClaimType = context
            .ScopeServiceProvider
            .GetRequiredService<IOptions<IdentityOptions>>().Value
            .ClaimsIdentity.SecurityStampClaimType;

        foreach (var claim in context.Principal.Claims)
        {
            if (claim.Type == SmartSoftwareClaimTypes.TenantId)
            {
                claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                continue;
            }

            if (claim.Type == SmartSoftwareClaimTypes.SessionId)
            {
                claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                continue;
            }

            switch (claim.Type)
            {
                case OpenIddictConstants.Claims.PreferredUsername:
                    claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken);
                    if (context.Principal.HasScope(OpenIddictConstants.Scopes.Profile))
                    {
                        claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                    }
                    break;

                case JwtRegisteredClaimNames.UniqueName:
                    claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken);
                    if (context.Principal.HasScope(OpenIddictConstants.Scopes.Profile))
                    {
                        claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                    }
                    break;

                case OpenIddictConstants.Claims.Email:
                    claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken);
                    if (context.Principal.HasScope(OpenIddictConstants.Scopes.Email))
                    {
                        claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                    }
                    break;

                case OpenIddictConstants.Claims.Role:
                    claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken);
                    if (context.Principal.HasScope(OpenIddictConstants.Scopes.Roles))
                    {
                        claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                    }
                    break;

                default:
                    // Never include the security stamp in the access and identity tokens, as it's a secret value.
                    if (claim.Type != securityStampClaimType)
                    {
                        claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken);
                    }
                    break;
            }
        }

        return Task.CompletedTask;
    }
}
