﻿using System.Threading.Tasks;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Guids;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.PermissionManagement.OpenIddict;

public class ApplicationPermissionManagementProvider : PermissionManagementProvider
{
    public override string Name => ClientPermissionValueProvider.ProviderName;

    public ApplicationPermissionManagementProvider(
        IPermissionGrantRepository permissionGrantRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant)
        : base(
            permissionGrantRepository,
            guidGenerator,
            currentTenant)
    {

    }

    public override Task<PermissionValueProviderGrantInfo> CheckAsync(string name, string providerName, string providerKey)
    {
        using (CurrentTenant.Change(null))
        {
            return base.CheckAsync(name, providerName, providerKey);
        }
    }

    protected override Task GrantAsync(string name, string providerKey)
    {
        using (CurrentTenant.Change(null))
        {
            return base.GrantAsync(name, providerKey);
        }
    }

    protected override Task RevokeAsync(string name, string providerKey)
    {
        using (CurrentTenant.Change(null))
        {
            return base.RevokeAsync(name, providerKey);
        }
    }

    public override Task SetAsync(string name, string providerKey, bool isGranted)
    {
        using (CurrentTenant.Change(null))
        {
            return base.SetAsync(name, providerKey, isGranted);
        }
    }
}
