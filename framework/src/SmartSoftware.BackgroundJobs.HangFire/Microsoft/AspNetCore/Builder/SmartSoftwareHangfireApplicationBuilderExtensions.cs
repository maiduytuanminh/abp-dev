using System;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.BackgroundJobs.Hangfire;

namespace Microsoft.AspNetCore.Builder;

public static class SmartSoftwareHangfireApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSmartSoftwareHangfireDashboard(
        this IApplicationBuilder app,
        string pathMatch = "/hangfire",
        Action<DashboardOptions>? configure = null,
        JobStorage? storage = null)
    {
        var options = app.ApplicationServices.GetRequiredService<SmartSoftwareDashboardOptionsProvider>().Get();
        configure?.Invoke(options);
        return app.UseHangfireDashboard(pathMatch, options, storage);
    }
}