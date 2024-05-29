using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.AntiForgery;

public class SmartSoftwareAutoValidateAntiforgeryTokenAuthorizationFilter : SmartSoftwareValidateAntiforgeryTokenAuthorizationFilter, ITransientDependency
{
    private readonly SmartSoftwareAntiForgeryOptions _options;

    public SmartSoftwareAutoValidateAntiforgeryTokenAuthorizationFilter(
        IAntiforgery antiforgery,
        SmartSoftwareAntiForgeryCookieNameProvider antiForgeryCookieNameProvider,
        IOptions<SmartSoftwareAntiForgeryOptions> options,
        ILogger<SmartSoftwareValidateAntiforgeryTokenAuthorizationFilter> logger)
        : base(
            antiforgery,
            antiForgeryCookieNameProvider,
            logger)
    {
        _options = options.Value;
    }

    protected override bool ShouldValidate(AuthorizationFilterContext context)
    {
        if (!_options.AutoValidate)
        {
            return false;
        }

        if (context.ActionDescriptor.IsControllerAction())
        {
            var controllerType = context.ActionDescriptor
                .AsControllerActionDescriptor()
                .ControllerTypeInfo
                .AsType();

            if (!_options.AutoValidateFilter(controllerType))
            {
                return false;
            }
        }

        if (IsIgnoredHttpMethod(context))
        {
            return false;
        }

        return base.ShouldValidate(context);
    }

    protected virtual bool IsIgnoredHttpMethod(AuthorizationFilterContext context)
    {
        return context.HttpContext
            .Request
            .Method
            .ToUpperInvariant()
            .IsIn(_options.AutoValidateIgnoredHttpMethods);
    }
}
