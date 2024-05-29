﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using SmartSoftware.AspNetCore.Middleware;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Authorization;
using SmartSoftware.DependencyInjection;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Http;
using SmartSoftware.Json;

namespace SmartSoftware.AspNetCore.ExceptionHandling;

public class SmartSoftwareExceptionHandlingMiddleware : SmartSoftwareMiddlewareBase, ITransientDependency
{
    private readonly ILogger<SmartSoftwareExceptionHandlingMiddleware> _logger;

    private readonly Func<object, Task> _clearCacheHeadersDelegate;

    public SmartSoftwareExceptionHandlingMiddleware(ILogger<SmartSoftwareExceptionHandlingMiddleware> logger)
    {
        _logger = logger;

        _clearCacheHeadersDelegate = ClearCacheHeaders;
    }

    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // We can't do anything if the response has already started, just abort.
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("An exception occurred, but response has already started!");
                throw;
            }

            if (context.Items["_SmartSoftwareActionInfo"] is SmartSoftwareActionInfoInHttpContext actionInfo)
            {
                if (actionInfo.IsObjectResult) //TODO: Align with SmartSoftwareExceptionFilter.ShouldHandleException!
                {
                    await HandleAndWrapException(context, ex);
                    return;
                }
            }

            throw;
        }
    }

    private async Task HandleAndWrapException(HttpContext httpContext, Exception exception)
    {
        _logger.LogException(exception);

        await httpContext
            .RequestServices
            .GetRequiredService<IExceptionNotifier>()
            .NotifyAsync(
                new ExceptionNotificationContext(exception)
            );

        if (exception is SmartSoftwareAuthorizationException)
        {
            await httpContext.RequestServices.GetRequiredService<ISmartSoftwareAuthorizationExceptionHandler>()
                .HandleAsync(exception.As<SmartSoftwareAuthorizationException>(), httpContext);
        }
        else
        {
            var errorInfoConverter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfoConverter>();
            var statusCodeFinder = httpContext.RequestServices.GetRequiredService<IHttpExceptionStatusCodeFinder>();
            var jsonSerializer = httpContext.RequestServices.GetRequiredService<IJsonSerializer>();
            var exceptionHandlingOptions = httpContext.RequestServices.GetRequiredService<IOptions<SmartSoftwareExceptionHandlingOptions>>().Value;

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCodeFinder.GetStatusCode(httpContext, exception);
            httpContext.Response.OnStarting(_clearCacheHeadersDelegate, httpContext.Response);
            httpContext.Response.Headers.Add(SmartSoftwareHttpConsts.SmartSoftwareErrorFormat, "true");
            httpContext.Response.Headers.Add("Content-Type", "application/json");

            await httpContext.Response.WriteAsync(
                jsonSerializer.Serialize(
                    new RemoteServiceErrorResponse(
                        errorInfoConverter.Convert(exception, options =>
                        {
                            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
                        })
                    )
                )
            );
        }
    }

    private Task ClearCacheHeaders(object state)
    {
        var response = (HttpResponse)state;

        response.Headers[HeaderNames.CacheControl] = "no-cache";
        response.Headers[HeaderNames.Pragma] = "no-cache";
        response.Headers[HeaderNames.Expires] = "-1";
        response.Headers.Remove(HeaderNames.ETag);

        return Task.CompletedTask;
    }
}
