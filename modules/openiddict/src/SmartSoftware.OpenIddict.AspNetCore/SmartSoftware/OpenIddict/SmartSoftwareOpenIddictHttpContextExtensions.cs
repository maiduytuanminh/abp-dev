using Microsoft.AspNetCore.Http;
using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;

namespace SmartSoftware.OpenIddict;

public static class SmartSoftwareOpenIddictHttpContextExtensions
{
    public static OpenIddictServerTransaction GetOpenIddictServerTransaction(this HttpContext context)
    {
        Check.NotNull(context, nameof(context));
        return context.Features.Get<OpenIddictServerAspNetCoreFeature>()?.Transaction;
    }
}
