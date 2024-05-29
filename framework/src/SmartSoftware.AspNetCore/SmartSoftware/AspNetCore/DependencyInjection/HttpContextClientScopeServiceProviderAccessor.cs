using System;
using Microsoft.AspNetCore.Http;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.DependencyInjection;

public class HttpContextClientScopeServiceProviderAccessor :
    IClientScopeServiceProviderAccessor,
    ISingletonDependency
{
    public IServiceProvider ServiceProvider {
        get {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new SmartSoftwareException("HttpContextClientScopeServiceProviderAccessor should only be used in a web request scope!");
            }

            return httpContext.RequestServices;
        }
    }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextClientScopeServiceProviderAccessor(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}
