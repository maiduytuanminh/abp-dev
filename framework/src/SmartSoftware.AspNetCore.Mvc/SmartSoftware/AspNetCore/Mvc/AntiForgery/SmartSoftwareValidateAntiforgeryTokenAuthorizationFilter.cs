﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.AntiForgery;

public class SmartSoftwareValidateAntiforgeryTokenAuthorizationFilter : IAsyncAuthorizationFilter, IAntiforgeryPolicy, ITransientDependency
{
    private IAntiforgery _antiforgery;
    private readonly SmartSoftwareAntiForgeryCookieNameProvider _antiForgeryCookieNameProvider;
    private readonly ILogger<SmartSoftwareValidateAntiforgeryTokenAuthorizationFilter> _logger;

    public SmartSoftwareValidateAntiforgeryTokenAuthorizationFilter(
        IAntiforgery antiforgery,
        SmartSoftwareAntiForgeryCookieNameProvider antiForgeryCookieNameProvider,
        ILogger<SmartSoftwareValidateAntiforgeryTokenAuthorizationFilter> logger)
    {
        _antiforgery = antiforgery;
        _logger = logger;
        _antiForgeryCookieNameProvider = antiForgeryCookieNameProvider;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (!context.IsEffectivePolicy<IAntiforgeryPolicy>(this))
        {
            _logger.LogInformation("Skipping the execution of current filter as its not the most effective filter implementing the policy " + typeof(IAntiforgeryPolicy));
            return;
        }

        if (ShouldValidate(context))
        {
            try
            {
                await _antiforgery.ValidateRequestAsync(context.HttpContext);
            }
            catch (AntiforgeryValidationException exception)
            {
                _logger.LogWarning(exception.Message, exception);
                context.Result = new AntiforgeryValidationFailedResult();
            }
        }
    }

    protected virtual bool ShouldValidate(AuthorizationFilterContext context)
    {
        var authCookieName = _antiForgeryCookieNameProvider.GetAuthCookieNameOrNull();

        //Always perform antiforgery validation when request contains authentication cookie
        if (authCookieName != null &&
            context.HttpContext.Request.Cookies.ContainsKey(authCookieName))
        {
            return true;
        }

        var antiForgeryCookieName = _antiForgeryCookieNameProvider.GetAntiForgeryCookieNameOrNull();

        //No need to validate if antiforgery cookie is not sent.
        //That means the request is sent from a non-browser client.
        //See https://github.com/aspnet/Antiforgery/issues/115
        if (antiForgeryCookieName != null &&
            !context.HttpContext.Request.Cookies.ContainsKey(antiForgeryCookieName))
        {
            return false;
        }

        // Anything else requires a token.
        return true;
    }
}
