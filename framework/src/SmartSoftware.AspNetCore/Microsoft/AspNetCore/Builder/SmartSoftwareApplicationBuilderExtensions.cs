using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSoftware;
using SmartSoftware.AspNetCore.Auditing;
using SmartSoftware.AspNetCore.ExceptionHandling;
using SmartSoftware.AspNetCore.Security;
using SmartSoftware.AspNetCore.Security.Claims;
using SmartSoftware.AspNetCore.Tracing;
using SmartSoftware.AspNetCore.Uow;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Threading;

namespace Microsoft.AspNetCore.Builder;

public static class SmartSoftwareApplicationBuilderExtensions
{
    private const string ExceptionHandlingMiddlewareMarker = "_SmartSoftwareExceptionHandlingMiddleware_Added";

    public async static Task InitializeApplicationAsync([NotNull] this IApplicationBuilder app)
    {
        Check.NotNull(app, nameof(app));

        app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
        var application = app.ApplicationServices.GetRequiredService<ISmartSoftwareApplicationWithExternalServiceProvider>();
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            AsyncHelper.RunSync(() => application.ShutdownAsync());
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        await application.InitializeAsync(app.ApplicationServices);
    }

    public static void InitializeApplication([NotNull] this IApplicationBuilder app)
    {
        Check.NotNull(app, nameof(app));

        app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
        var application = app.ApplicationServices.GetRequiredService<ISmartSoftwareApplicationWithExternalServiceProvider>();
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() =>
        {
            application.Shutdown();
        });

        applicationLifetime.ApplicationStopped.Register(() =>
        {
            application.Dispose();
        });

        application.Initialize(app.ApplicationServices);
    }

    public static IApplicationBuilder UseAuditing(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<SmartSoftwareAuditingMiddleware>();
    }

    public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
    {
        return app
            .UseSmartSoftwareExceptionHandling()
            .UseMiddleware<SmartSoftwareUnitOfWorkMiddleware>();
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<SmartSoftwareCorrelationIdMiddleware>();
    }

    public static IApplicationBuilder UseSmartSoftwareRequestLocalization(this IApplicationBuilder app,
        Action<RequestLocalizationOptions>? optionsAction = null)
    {
        app.ApplicationServices
            .GetRequiredService<ISmartSoftwareRequestLocalizationOptionsProvider>()
            .InitLocalizationOptions(optionsAction);

        return app.UseMiddleware<SmartSoftwareRequestLocalizationMiddleware>();
    }

    public static IApplicationBuilder UseSmartSoftwareExceptionHandling(this IApplicationBuilder app)
    {
        if (app.Properties.ContainsKey(ExceptionHandlingMiddlewareMarker))
        {
            return app;
        }

        app.Properties[ExceptionHandlingMiddlewareMarker] = true;
        return app.UseMiddleware<SmartSoftwareExceptionHandlingMiddleware>();
    }

    [Obsolete("Replace with SmartSoftwareClaimsTransformation")]
    public static IApplicationBuilder UseSmartSoftwareClaimsMap(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SmartSoftwareClaimsMapMiddleware>();
    }

    public static IApplicationBuilder UseSmartSoftwareSecurityHeaders(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SmartSoftwareSecurityHeadersMiddleware>();
    }

    public static IApplicationBuilder UseDynamicClaims(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SmartSoftwareDynamicClaimsMiddleware>();
    }
}
