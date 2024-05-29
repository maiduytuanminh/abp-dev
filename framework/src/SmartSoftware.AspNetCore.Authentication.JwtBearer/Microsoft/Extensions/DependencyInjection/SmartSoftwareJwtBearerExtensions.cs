using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SmartSoftware.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection;

public static class SmartSoftwareJwtBearerExtensions
{
    public static AuthenticationBuilder AddSmartSoftwareJwtBearer(this AuthenticationBuilder builder)
        => builder.AddSmartSoftwareJwtBearer(JwtBearerDefaults.AuthenticationScheme, _ => { });

    public static AuthenticationBuilder AddSmartSoftwareJwtBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
        => builder.AddSmartSoftwareJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddSmartSoftwareJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtBearerOptions> configureOptions)
        => builder.AddSmartSoftwareJwtBearer(authenticationScheme, "Bearer", configureOptions);

    public static AuthenticationBuilder AddSmartSoftwareJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JwtBearerOptions> configureOptions)
    {
        builder.Services.Configure<SmartSoftwareClaimsPrincipalFactoryOptions>(options =>
        {
            var jwtBearerOption = new JwtBearerOptions();
            configureOptions?.Invoke(jwtBearerOption);
            if (!jwtBearerOption.Authority.IsNullOrEmpty())
            {
                options.RemoteRefreshUrl = jwtBearerOption.Authority.RemovePostFix("/") + options.RemoteRefreshUrl;
            }
        });

        return builder.AddJwtBearer(authenticationScheme, displayName, options =>
        {
            configureOptions?.Invoke(options);
        });
    }
}
