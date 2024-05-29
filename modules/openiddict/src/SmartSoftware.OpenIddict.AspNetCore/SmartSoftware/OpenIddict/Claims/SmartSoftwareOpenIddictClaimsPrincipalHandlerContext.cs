using System;
using System.Security.Claims;
using OpenIddict.Abstractions;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictClaimsPrincipalHandlerContext
{
     public IServiceProvider ScopeServiceProvider { get; }

     public OpenIddictRequest OpenIddictRequest { get; }

     public ClaimsPrincipal Principal { get;}

     public SmartSoftwareOpenIddictClaimsPrincipalHandlerContext(IServiceProvider scopeServiceProvider, OpenIddictRequest openIddictRequest, ClaimsPrincipal principal)
     {
          ScopeServiceProvider = scopeServiceProvider;
          OpenIddictRequest = openIddictRequest;
          Principal = principal;
     }
}
