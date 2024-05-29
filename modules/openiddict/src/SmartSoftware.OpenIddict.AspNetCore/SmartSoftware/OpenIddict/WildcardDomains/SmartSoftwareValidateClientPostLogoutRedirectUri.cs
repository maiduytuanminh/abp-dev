using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server;

namespace SmartSoftware.OpenIddict.WildcardDomains;

public class SmartSoftwareValidateClientPostLogoutRedirectUri : SmartSoftwareOpenIddictWildcardDomainBase<SmartSoftwareValidateClientPostLogoutRedirectUri, OpenIddictServerHandlers.Session.ValidateClientPostLogoutRedirectUri, OpenIddictServerEvents.ValidateLogoutRequestContext>
{
    public static OpenIddictServerHandlerDescriptor Descriptor { get; }
        = OpenIddictServerHandlerDescriptor.CreateBuilder<OpenIddictServerEvents.ValidateLogoutRequestContext>()
            .AddFilter<OpenIddictServerHandlerFilters.RequireDegradedModeDisabled>()
            .AddFilter<OpenIddictServerHandlerFilters.RequirePostLogoutRedirectUriParameter>()
            .UseScopedHandler<SmartSoftwareValidateClientPostLogoutRedirectUri>()
            .SetOrder(OpenIddictServerHandlers.Session.ValidatePostLogoutRedirectUriParameter.Descriptor.Order + 1_000)
            .SetType(OpenIddictServerHandlerType.BuiltIn)
            .Build();

    public SmartSoftwareValidateClientPostLogoutRedirectUri(
        IOptions<SmartSoftwareOpenIddictWildcardDomainOptions> wildcardDomainsOptions,
        IOpenIddictApplicationManager applicationManager)
        : base(wildcardDomainsOptions, new OpenIddictServerHandlers.Session.ValidateClientPostLogoutRedirectUri(applicationManager))
    {
        OriginalHandler = new OpenIddictServerHandlers.Session.ValidateClientPostLogoutRedirectUri(applicationManager);
    }

    public async override ValueTask HandleAsync(OpenIddictServerEvents.ValidateLogoutRequestContext context)
    {
        Check.NotNull(context, nameof(context));
        Check.NotNullOrEmpty(context.PostLogoutRedirectUri, nameof(context.PostLogoutRedirectUri));

        if (await CheckWildcardDomainAsync(context.PostLogoutRedirectUri))
        {
            return;
        }

        await OriginalHandler.HandleAsync(context);
    }
}
