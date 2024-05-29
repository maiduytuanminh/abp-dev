﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Authorization;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.ExceptionHandling;

public class DefaultSmartSoftwareAuthorizationExceptionHandler : ISmartSoftwareAuthorizationExceptionHandler, ITransientDependency
{
    public virtual async Task HandleAsync(SmartSoftwareAuthorizationException exception, HttpContext httpContext)
    {
        var handlerOptions = httpContext.RequestServices.GetRequiredService<IOptions<SmartSoftwareAuthorizationExceptionHandlerOptions>>().Value;
        var isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
        var authenticationSchemeProvider = httpContext.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

        AuthenticationScheme? scheme = null;

        if (!handlerOptions.AuthenticationScheme.IsNullOrWhiteSpace())
        {
            scheme = await authenticationSchemeProvider.GetSchemeAsync(handlerOptions.AuthenticationScheme!);
            if (scheme == null)
            {
                throw new SmartSoftwareException($"No authentication scheme named {handlerOptions.AuthenticationScheme} was found.");
            }
        }
        else
        {
            if (isAuthenticated)
            {
                scheme = await authenticationSchemeProvider.GetDefaultForbidSchemeAsync();
                if (scheme == null)
                {
                    throw new SmartSoftwareException($"There was no DefaultForbidScheme found.");
                }
            }
            else
            {
                scheme = await authenticationSchemeProvider.GetDefaultChallengeSchemeAsync();
                if (scheme == null)
                {
                    throw new SmartSoftwareException($"There was no DefaultChallengeScheme found.");
                }
            }
        }

        var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
        var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name);
        if (handler == null)
        {
            throw new SmartSoftwareException($"No handler of {scheme.Name} was found.");
        }

        if (isAuthenticated)
        {
            await handler.ForbidAsync(null);
        }
        else
        {
            await handler.ChallengeAsync(null);
        }
    }
}
