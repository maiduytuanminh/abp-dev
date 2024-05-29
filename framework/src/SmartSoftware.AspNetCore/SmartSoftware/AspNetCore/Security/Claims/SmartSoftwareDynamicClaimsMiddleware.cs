using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Middleware;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Security.Claims;

public class SmartSoftwareDynamicClaimsMiddleware : SmartSoftwareMiddlewareBase, ITransientDependency
{
    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            if (context.RequestServices.GetRequiredService<IOptions<SmartSoftwareClaimsPrincipalFactoryOptions>>().Value.IsDynamicClaimsEnabled)
            {
                var authenticationType = context.User.Identity.AuthenticationType;
                var ssClaimsPrincipalFactory = context.RequestServices.GetRequiredService<ISmartSoftwareClaimsPrincipalFactory>();
                context.User = await ssClaimsPrincipalFactory.CreateDynamicAsync(context.User);

                if (context.User.Identity?.IsAuthenticated == false)
                {
                    if (!authenticationType.IsNullOrWhiteSpace())
                    {
                        var authenticationSchemeProvider = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
                        var scheme = await authenticationSchemeProvider.GetSchemeAsync(authenticationType);
                        if (scheme != null)
                        {
                            await context.SignOutAsync(scheme.Name);
                        }
                    }
                    else
                    {
                        await context.SignOutAsync();
                    }
                }
            }
        }

        await next(context);
    }
}
