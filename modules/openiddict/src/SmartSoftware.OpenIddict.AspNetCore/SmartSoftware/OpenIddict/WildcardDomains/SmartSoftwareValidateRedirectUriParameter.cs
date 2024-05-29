using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenIddict.Server;

namespace SmartSoftware.OpenIddict.WildcardDomains;

public class SmartSoftwareValidateRedirectUriParameter : SmartSoftwareOpenIddictWildcardDomainBase<SmartSoftwareValidateRedirectUriParameter, OpenIddictServerHandlers.Authentication.ValidateRedirectUriParameter, OpenIddictServerEvents.ValidateAuthorizationRequestContext>
{
    public static OpenIddictServerHandlerDescriptor Descriptor { get; }
        = OpenIddictServerHandlerDescriptor.CreateBuilder<OpenIddictServerEvents.ValidateAuthorizationRequestContext>()
            .UseSingletonHandler<SmartSoftwareValidateRedirectUriParameter>()
            .SetOrder(OpenIddictServerHandlers.Authentication.ValidateClientIdParameter.Descriptor.Order + 1_000)
            .SetType(OpenIddictServerHandlerType.BuiltIn)
            .Build();

    public SmartSoftwareValidateRedirectUriParameter(IOptions<SmartSoftwareOpenIddictWildcardDomainOptions> wildcardDomainsOptions)
        : base(wildcardDomainsOptions, new OpenIddictServerHandlers.Authentication.ValidateRedirectUriParameter())
    {
    }

    public async override ValueTask HandleAsync(OpenIddictServerEvents.ValidateAuthorizationRequestContext context)
    {
        Check.NotNull(context, nameof(context));

        if (!string.IsNullOrEmpty(context.RedirectUri) && await CheckWildcardDomainAsync(context.RedirectUri))
        {
            return;
        }

        await OriginalHandler.HandleAsync(context);
    }
}
