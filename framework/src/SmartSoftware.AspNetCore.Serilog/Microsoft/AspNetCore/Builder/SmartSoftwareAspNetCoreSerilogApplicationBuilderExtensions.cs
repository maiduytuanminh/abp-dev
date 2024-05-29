using SmartSoftware.AspNetCore.Serilog;

namespace Microsoft.AspNetCore.Builder;

public static class SmartSoftwareAspNetCoreSerilogApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSmartSoftwareSerilogEnrichers(this IApplicationBuilder app)
    {
        return app
            .UseMiddleware<SmartSoftwareSerilogMiddleware>();
    }
}
