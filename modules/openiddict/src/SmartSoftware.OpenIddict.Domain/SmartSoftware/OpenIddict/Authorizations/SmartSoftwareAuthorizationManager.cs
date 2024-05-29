using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;

namespace SmartSoftware.OpenIddict.Authorizations;

public class SmartSoftwareAuthorizationManager : OpenIddictAuthorizationManager<OpenIddictAuthorizationModel>
{
    protected SmartSoftwareOpenIddictIdentifierConverter IdentifierConverter { get; }

    public SmartSoftwareAuthorizationManager(
        [NotNull] [ItemNotNull] IOpenIddictAuthorizationCache<OpenIddictAuthorizationModel> cache,
        [NotNull] [ItemNotNull] ILogger<OpenIddictAuthorizationManager<OpenIddictAuthorizationModel>> logger,
        [NotNull] [ItemNotNull] IOptionsMonitor<OpenIddictCoreOptions> options,
        [NotNull] IOpenIddictAuthorizationStoreResolver resolver,
        SmartSoftwareOpenIddictIdentifierConverter identifierConverter)
        : base(cache, logger, options, resolver)
    {
        IdentifierConverter = identifierConverter;
    }

    public async override ValueTask UpdateAsync(OpenIddictAuthorizationModel authorization, CancellationToken cancellationToken = default)
    {
        if (!Options.CurrentValue.DisableEntityCaching)
        {
            var entity = await Store.FindByIdAsync(IdentifierConverter.ToString(authorization.Id), cancellationToken);
            if (entity != null)
            {
                await Cache.RemoveAsync(entity, cancellationToken);
            }
        }

        await base.UpdateAsync(authorization, cancellationToken);
    }
}
