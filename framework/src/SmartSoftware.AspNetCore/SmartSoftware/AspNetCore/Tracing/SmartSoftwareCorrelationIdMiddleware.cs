﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Middleware;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Tracing;

namespace SmartSoftware.AspNetCore.Tracing;

public class SmartSoftwareCorrelationIdMiddleware : SmartSoftwareMiddlewareBase, ITransientDependency
{
    private readonly SmartSoftwareCorrelationIdOptions _options;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public SmartSoftwareCorrelationIdMiddleware(IOptions<SmartSoftwareCorrelationIdOptions> options,
        ICorrelationIdProvider correlationIdProvider)
    {
        _options = options.Value;
        _correlationIdProvider = correlationIdProvider;
    }

    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = GetCorrelationIdFromRequest(context);
        using (_correlationIdProvider.Change(correlationId))
        {
            CheckAndSetCorrelationIdOnResponse(context, _options, correlationId);
            await next(context);
        }
    }

    protected virtual string? GetCorrelationIdFromRequest(HttpContext context)
    {
        var correlationId = context.Request.Headers[_options.HttpHeaderName];
        if (correlationId.IsNullOrEmpty())
        {
            correlationId = Guid.NewGuid().ToString("N");
            context.Request.Headers[_options.HttpHeaderName] = correlationId;
        }

        return correlationId;
    }

    protected virtual void CheckAndSetCorrelationIdOnResponse(
        HttpContext httpContext,
        SmartSoftwareCorrelationIdOptions options,
        string? correlationId)
    {
        httpContext.Response.OnStarting(() =>
        {
            if (options.SetResponseHeader && !httpContext.Response.Headers.ContainsKey(options.HttpHeaderName) && !string.IsNullOrWhiteSpace(correlationId))
            {
                httpContext.Response.Headers[options.HttpHeaderName] = correlationId;
            }

            return Task.CompletedTask;
        });
    }
}
