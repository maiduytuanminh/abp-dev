﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;

namespace SmartSoftware.OpenIddict.Applications;

public class SmartSoftwareApplicationManager : OpenIddictApplicationManager<OpenIddictApplicationModel>, ISmartSoftwareApplicationManager
{
    protected SmartSoftwareOpenIddictIdentifierConverter IdentifierConverter { get; }

    public SmartSoftwareApplicationManager(
        [NotNull] IOpenIddictApplicationCache<OpenIddictApplicationModel> cache,
        [NotNull] ILogger<SmartSoftwareApplicationManager> logger,
        [NotNull] IOptionsMonitor<OpenIddictCoreOptions> options,
        [NotNull] IOpenIddictApplicationStoreResolver resolver,
        SmartSoftwareOpenIddictIdentifierConverter identifierConverter)
        : base(cache, logger, options, resolver)
    {
        IdentifierConverter = identifierConverter;
    }

    public async override ValueTask UpdateAsync(OpenIddictApplicationModel application, CancellationToken cancellationToken = default)
    {
        if (!Options.CurrentValue.DisableEntityCaching)
        {
            var entity = await Store.FindByIdAsync(IdentifierConverter.ToString(application.Id), cancellationToken);
            if (entity != null)
            {
                await Cache.RemoveAsync(entity, cancellationToken);
            }
        }

        await base.UpdateAsync(application, cancellationToken);
    }

    public async override ValueTask PopulateAsync(OpenIddictApplicationDescriptor descriptor, OpenIddictApplicationModel application, CancellationToken cancellationToken = default)
    {
        await base.PopulateAsync(descriptor, application, cancellationToken);

        if (descriptor is SmartSoftwareApplicationDescriptor model)
        {
            model.ClientUri = application.ClientUri;
            model.LogoUri = application.LogoUri;
        }
    }

    public async override ValueTask PopulateAsync(OpenIddictApplicationModel application, OpenIddictApplicationDescriptor descriptor, CancellationToken cancellationToken = default)
    {
        await base.PopulateAsync(application, descriptor, cancellationToken);

        if (descriptor is SmartSoftwareApplicationDescriptor model)
        {
            application.ClientUri = model.ClientUri;
            application.LogoUri = model.LogoUri;
        }
    }

    public virtual async ValueTask<string> GetClientUriAsync(object application, CancellationToken cancellationToken = default)
    {
        Check.NotNull(application, nameof(application));
        Check.AssignableTo<ISmartSoftwareOpenIdApplicationStore>(application.GetType(), nameof(application));

        return await Store.As<ISmartSoftwareOpenIdApplicationStore>().GetClientUriAsync(application.As<OpenIddictApplicationModel>(), cancellationToken);
    }

    public virtual async ValueTask<string> GetLogoUriAsync(object application, CancellationToken cancellationToken = default)
    {
        Check.NotNull(application, nameof(application));
        Check.AssignableTo<ISmartSoftwareOpenIdApplicationStore>(application.GetType(), nameof(application));

        return await Store.As<ISmartSoftwareOpenIdApplicationStore>().GetLogoUriAsync(application.As<OpenIddictApplicationModel>(), cancellationToken);
    }
}
