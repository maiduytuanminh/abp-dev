using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SmartSoftware.AspNetCore.Authentication.OpenIdConnect;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareOpenIdConnectExtensions
{
    public static AuthenticationBuilder AddSmartSoftwareOpenIdConnect(this AuthenticationBuilder builder)
        => builder.AddSmartSoftwareOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, _ => { });

    public static AuthenticationBuilder AddSmartSoftwareOpenIdConnect(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)
        => builder.AddSmartSoftwareOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddSmartSoftwareOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)
        => builder.AddSmartSoftwareOpenIdConnect(authenticationScheme, OpenIdConnectDefaults.DisplayName, configureOptions);

    public static AuthenticationBuilder AddSmartSoftwareOpenIdConnect(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<OpenIdConnectOptions> configureOptions)
    {
        builder.Services.Configure<SmartSoftwareClaimsPrincipalFactoryOptions>(options =>
        {
            var openIdConnectOptions = new OpenIdConnectOptions();
            configureOptions?.Invoke(openIdConnectOptions);
            if (!openIdConnectOptions.Authority.IsNullOrEmpty())
            {
                options.RemoteRefreshUrl = openIdConnectOptions.Authority.RemovePostFix("/") + options.RemoteRefreshUrl;
            }
        });

        return builder.AddOpenIdConnect(authenticationScheme, displayName, options =>
        {
            options.ClaimActions.MapSmartSoftwareClaimTypes();

            options.Events ??= new OpenIdConnectEvents();
            var authorizationCodeReceived = options.Events.OnAuthorizationCodeReceived ?? (_ => Task.CompletedTask);

            options.Events.OnAuthorizationCodeReceived = receivedContext =>
            {
                SetSmartSoftwareTenantId(receivedContext);
                return authorizationCodeReceived.Invoke(receivedContext);
            };

            options.AccessDeniedPath = "/";

            options.Events.OnTokenValidated = async (context) =>
            {
                var client = context.HttpContext.RequestServices.GetRequiredService<IOpenIdLocalUserCreationClient>();
                try
                {
                    await client.CreateOrUpdateAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = context.HttpContext.RequestServices.GetService<ILogger<SmartSoftwareAspNetCoreAuthenticationOpenIdConnectModule>>();
                    logger?.LogException(ex);
                }
            };

            configureOptions?.Invoke(options);
        });
    }

    private static void SetSmartSoftwareTenantId(AuthorizationCodeReceivedContext receivedContext)
    {
        var tenantKey = receivedContext.HttpContext.RequestServices
            .GetRequiredService<IOptions<SmartSoftwareAspNetCoreMultiTenancyOptions>>().Value.TenantKey;

        if (receivedContext.Request.Cookies.ContainsKey(tenantKey))
        {
            receivedContext.TokenEndpointRequest?.SetParameter(tenantKey, receivedContext.Request.Cookies[tenantKey]);
        }
    }
}
