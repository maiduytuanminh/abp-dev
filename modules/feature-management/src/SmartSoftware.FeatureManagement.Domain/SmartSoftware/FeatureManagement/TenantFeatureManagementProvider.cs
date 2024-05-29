﻿using System;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Features;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.FeatureManagement;

public class TenantFeatureManagementProvider : FeatureManagementProvider, ITransientDependency
{
    public override string Name => TenantFeatureValueProvider.ProviderName;

    protected ICurrentTenant CurrentTenant { get; }

    public TenantFeatureManagementProvider(
        IFeatureManagementStore store,
        ICurrentTenant currentTenant)
        : base(store)
    {
        CurrentTenant = currentTenant;
    }

    public override Task<IAsyncDisposable> HandleContextAsync(string providerName, string providerKey)
    {
        if (providerName == Name && !providerKey.IsNullOrWhiteSpace())
        {
            if (Guid.TryParse(providerKey, out var tenantId))
            {
                var disposable = CurrentTenant.Change(tenantId);
                return Task.FromResult<IAsyncDisposable>(new AsyncDisposeFunc(() =>
                {
                    disposable.Dispose();
                    return Task.CompletedTask;
                }));
            }
        }

        return base.HandleContextAsync(providerName, providerKey);
    }

    protected override Task<string> NormalizeProviderKeyAsync(string providerKey)
    {
        if (providerKey != null)
        {
            return Task.FromResult(providerKey);
        }

        return Task.FromResult(CurrentTenant.Id?.ToString());
    }
}
