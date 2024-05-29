using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;

namespace SmartSoftware.OpenIddict.Scopes;

public class SmartSoftwareScopeManager : OpenIddictScopeManager<OpenIddictScopeModel>
{
    protected SmartSoftwareOpenIddictIdentifierConverter IdentifierConverter { get; }

    public SmartSoftwareScopeManager(
        [NotNull] [ItemNotNull] IOpenIddictScopeCache<OpenIddictScopeModel> cache,
        [NotNull] [ItemNotNull] ILogger<OpenIddictScopeManager<OpenIddictScopeModel>> logger,
        [NotNull] [ItemNotNull] IOptionsMonitor<OpenIddictCoreOptions> options,
        [NotNull] IOpenIddictScopeStoreResolver resolver,
        SmartSoftwareOpenIddictIdentifierConverter identifierConverter)
        : base(cache, logger, options, resolver)
    {
        IdentifierConverter = identifierConverter;
    }

    public async override ValueTask UpdateAsync(OpenIddictScopeModel scope, CancellationToken cancellationToken = default)
    {
        if (!Options.CurrentValue.DisableEntityCaching)
        {
            var entity = await Store.FindByIdAsync(IdentifierConverter.ToString(scope.Id), cancellationToken);
            if (entity != null)
            {
                await Cache.RemoveAsync(entity, cancellationToken);
            }
        }

        await base.UpdateAsync(scope, cancellationToken);
    }
}
