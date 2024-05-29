using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartSoftware.AspNetCore.Middleware;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc;

[Obsolete("Use FakeAuthenticationScheme instead.")]
public class FakeAuthenticationMiddleware : SmartSoftwareMiddlewareBase, ITransientDependency
{
    private readonly FakeUserClaims _fakeUserClaims;

    public FakeAuthenticationMiddleware(FakeUserClaims fakeUserClaims)
    {
        _fakeUserClaims = fakeUserClaims;
    }

    public async override Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_fakeUserClaims.Claims.Any())
        {
            context.User = new ClaimsPrincipal(new List<ClaimsIdentity>
                {
                    new ClaimsIdentity(_fakeUserClaims.Claims, "FakeSchema")
                });
        }

        await next(context);
    }
}
