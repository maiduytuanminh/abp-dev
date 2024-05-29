﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using SmartSoftware.OpenIddict.ExtensionGrantTypes;

namespace SmartSoftware.OpenIddict.Controllers;

[Route("connect/token")]
[IgnoreAntiforgeryToken]
[ApiExplorerSettings(IgnoreApi = true)]
public partial class TokenController : SmartSoftwareOpenIdDictControllerBase
{
    [HttpGet, HttpPost, Produces("application/json")]
    public virtual async Task<IActionResult> HandleAsync()
    {
        var request = await GetOpenIddictServerRequestAsync(HttpContext);

        if (request.IsPasswordGrantType())
        {
            return await HandlePasswordAsync(request);
        }

        if (request.IsAuthorizationCodeGrantType() )
        {
            return await HandleAuthorizationCodeAsync(request);
        }

        if (request.IsRefreshTokenGrantType() )
        {
            return await HandleRefreshTokenAsync(request);
        }

        if (request.IsDeviceCodeGrantType() )
        {
            return await HandleDeviceCodeAsync(request);
        }

        if (request.IsClientCredentialsGrantType())
        {
            return await HandleClientCredentialsAsync(request);
        }

        var extensionGrantsOptions = HttpContext.RequestServices.GetRequiredService<IOptions<SmartSoftwareOpenIddictExtensionGrantsOptions>>();
        var extensionTokenGrant = extensionGrantsOptions.Value.Find<ITokenExtensionGrant>(request.GrantType);
        if (extensionTokenGrant != null)
        {
            return await extensionTokenGrant.HandleAsync(new ExtensionGrantContext(HttpContext, request));
        }

        throw new SmartSoftwareException(string.Format(L["TheSpecifiedGrantTypeIsNotImplemented"], request.GrantType));
    }
}
