using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict.Scopes;
using SmartSoftware.OpenIddict.WildcardDomains;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.OpenIddict;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule),
    typeof(SmartSoftwareAspNetCoreMultiTenancyModule),
    typeof(SmartSoftwareOpenIddictDomainModule)
)]
public class SmartSoftwareOpenIddictAspNetCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        AddOpenIddictServer(context.Services);

        Configure<SmartSoftwareOpenIddictClaimsPrincipalOptions>(options =>
        {
            options.ClaimsPrincipalHandlers.Add<SmartSoftwareDynamicClaimsOpenIddictClaimsPrincipalHandler>();
            options.ClaimsPrincipalHandlers.Add<SmartSoftwareDefaultOpenIddictClaimsPrincipalHandler>();
        });

        Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationFormats.Add("/SmartSoftware/OpenIddict/Views/{1}/{0}.cshtml");
        });
    }

    private void AddOpenIddictServer(IServiceCollection services)
    {
        var builderOptions = services.ExecutePreConfiguredActions<SmartSoftwareOpenIddictAspNetCoreOptions>();

        if (builderOptions.UpdateSmartSoftwareClaimTypes)
        {
            SmartSoftwareClaimTypes.UserId = OpenIddictConstants.Claims.Subject;
            SmartSoftwareClaimTypes.Role = OpenIddictConstants.Claims.Role;
            SmartSoftwareClaimTypes.UserName = OpenIddictConstants.Claims.PreferredUsername;
            SmartSoftwareClaimTypes.Name = OpenIddictConstants.Claims.GivenName;
            SmartSoftwareClaimTypes.SurName = OpenIddictConstants.Claims.FamilyName;
            SmartSoftwareClaimTypes.PhoneNumber = OpenIddictConstants.Claims.PhoneNumber;
            SmartSoftwareClaimTypes.PhoneNumberVerified = OpenIddictConstants.Claims.PhoneNumberVerified;
            SmartSoftwareClaimTypes.Email = OpenIddictConstants.Claims.Email;
            SmartSoftwareClaimTypes.EmailVerified = OpenIddictConstants.Claims.EmailVerified;
            SmartSoftwareClaimTypes.ClientId = OpenIddictConstants.Claims.ClientId;
        }

        var openIddictBuilder = services.AddOpenIddict()
            .AddServer(builder =>
            {
                builder
                    .SetAuthorizationEndpointUris("connect/authorize", "connect/authorize/callback")
                    // .well-known/oauth-authorization-server
                    // .well-known/openid-configuration
                    //.SetConfigurationEndpointUris()
                    // .well-known/jwks
                    //.SetCryptographyEndpointUris()
                    .SetDeviceEndpointUris("device")
                    .SetIntrospectionEndpointUris("connect/introspect")
                    .SetLogoutEndpointUris("connect/logout")
                    .SetRevocationEndpointUris("connect/revocat")
                    .SetTokenEndpointUris("connect/token")
                    .SetUserinfoEndpointUris("connect/userinfo")
                    .SetVerificationEndpointUris("connect/verify");

                builder
                    .AllowAuthorizationCodeFlow()
                    .AllowHybridFlow()
                    .AllowImplicitFlow()
                    .AllowPasswordFlow()
                    .AllowClientCredentialsFlow()
                    .AllowRefreshTokenFlow()
                    .AllowDeviceCodeFlow()
                    .AllowNoneFlow();

                builder.RegisterScopes(new[]
                {
                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Email,
                    OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.Phone,
                    OpenIddictConstants.Scopes.Roles,
                    OpenIddictConstants.Scopes.Address,
                    OpenIddictConstants.Scopes.OfflineAccess
                });

                builder.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableVerificationEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();

                if (builderOptions.AddDevelopmentEncryptionAndSigningCertificate)
                {
                    builder
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();
                }

                builder.DisableAccessTokenEncryption();

                var wildcardDomainsOptions = services.ExecutePreConfiguredActions<SmartSoftwareOpenIddictWildcardDomainOptions>();
                if (wildcardDomainsOptions.EnableWildcardDomainSupport)
                {
                    var preActions = services.GetPreConfigureActions<SmartSoftwareOpenIddictWildcardDomainOptions>();

                    Configure<SmartSoftwareOpenIddictWildcardDomainOptions>(options =>
                    {
                        preActions.Configure(options);
                    });

                    builder.RemoveEventHandler(OpenIddictServerHandlers.Authentication.ValidateClientRedirectUri.Descriptor);
                    builder.AddEventHandler(SmartSoftwareValidateClientRedirectUri.Descriptor);

                    builder.RemoveEventHandler(OpenIddictServerHandlers.Authentication.ValidateRedirectUriParameter.Descriptor);
                    builder.AddEventHandler(SmartSoftwareValidateRedirectUriParameter.Descriptor);

                    builder.RemoveEventHandler(OpenIddictServerHandlers.Session.ValidateClientPostLogoutRedirectUri.Descriptor);
                    builder.AddEventHandler(SmartSoftwareValidateClientPostLogoutRedirectUri.Descriptor);

                    builder.RemoveEventHandler(OpenIddictServerHandlers.Session.ValidatePostLogoutRedirectUriParameter.Descriptor);
                    builder.AddEventHandler(SmartSoftwareValidatePostLogoutRedirectUriParameter.Descriptor);

                    builder.RemoveEventHandler(OpenIddictServerHandlers.Session.ValidateAuthorizedParty.Descriptor);
                    builder.AddEventHandler(SmartSoftwareValidateAuthorizedParty.Descriptor);
                }

                builder.AddEventHandler(RemoveClaimsFromClientCredentialsGrantType.Descriptor);
                builder.AddEventHandler(AttachScopes.Descriptor);

                services.ExecutePreConfiguredActions(builder);
            });

        services.ExecutePreConfiguredActions(openIddictBuilder);
    }
}
