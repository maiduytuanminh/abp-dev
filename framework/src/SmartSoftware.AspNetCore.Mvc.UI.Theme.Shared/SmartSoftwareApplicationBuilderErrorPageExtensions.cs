using Microsoft.AspNetCore.Builder;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;

public static class SmartSoftwareApplicationBuilderErrorPageExtensions
{
    public static IApplicationBuilder UseErrorPage(this IApplicationBuilder app)
    {
        return app
            .UseStatusCodePagesWithRedirects("~/Error?httpStatusCode={0}")
            .UseExceptionHandler("/Error");
    }
}
