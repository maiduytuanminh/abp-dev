using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using SmartSoftware.Swashbuckle;

namespace Microsoft.AspNetCore.Builder;

public static class SmartSoftwareSwaggerUIBuilderExtensions
{
    public static IApplicationBuilder UseSmartSoftwareSwaggerUI(
        this IApplicationBuilder app,
        Action<SwaggerUIOptions>? setupAction = null)
    {
        var resolver = app.ApplicationServices.GetService<ISwaggerHtmlResolver>();

        return app.UseSwaggerUI(options =>
        {
            options.InjectJavascript("ui/ss.js");
            options.InjectJavascript("ui/ss.swagger.js");
            options.IndexStream = () => resolver?.Resolver();

            setupAction?.Invoke(options);
        });
    }
}
