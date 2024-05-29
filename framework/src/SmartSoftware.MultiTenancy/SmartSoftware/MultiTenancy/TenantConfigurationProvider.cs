﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy.Localization;

namespace SmartSoftware.MultiTenancy;

public class TenantConfigurationProvider : ITenantConfigurationProvider, ITransientDependency
{
    protected virtual ITenantResolver TenantResolver { get; }
    protected virtual ITenantStore TenantStore { get; }
    protected virtual ITenantNormalizer TenantNormalizer { get; }
    protected virtual ITenantResolveResultAccessor TenantResolveResultAccessor { get; }
    protected virtual IStringLocalizer<SmartSoftwareMultiTenancyResource> StringLocalizer { get; }

    public TenantConfigurationProvider(
        ITenantResolver tenantResolver,
        ITenantStore tenantStore,
        ITenantResolveResultAccessor tenantResolveResultAccessor,
        IStringLocalizer<SmartSoftwareMultiTenancyResource> stringLocalizer,
        ITenantNormalizer tenantNormalizer)
    {
        TenantResolver = tenantResolver;
        TenantStore = tenantStore;
        TenantNormalizer = tenantNormalizer;
        TenantResolveResultAccessor = tenantResolveResultAccessor;
        StringLocalizer = stringLocalizer;
    }

    public virtual async Task<TenantConfiguration?> GetAsync(bool saveResolveResult = false)
    {
        var resolveResult = await TenantResolver.ResolveTenantIdOrNameAsync();

        if (saveResolveResult)
        {
            TenantResolveResultAccessor.Result = resolveResult;
        }

        TenantConfiguration? tenant = null;
        if (resolveResult.TenantIdOrName != null)
        {
            tenant = await FindTenantAsync(resolveResult.TenantIdOrName);

            if (tenant == null)
            {
                throw new BusinessException(
                    code: "SmartSoftwareIo.MultiTenancy:010001",
                    message: StringLocalizer["TenantNotFoundMessage"],
                    details: StringLocalizer["TenantNotFoundDetails", resolveResult.TenantIdOrName]
                );
            }

            if (!tenant.IsActive)
            {
                throw new BusinessException(
                    code: "SmartSoftwareIo.MultiTenancy:010002",
                    message: StringLocalizer["TenantNotActiveMessage"],
                    details: StringLocalizer["TenantNotActiveDetails", resolveResult.TenantIdOrName]
                );
            }
        }

        return tenant;
    }

    protected virtual async Task<TenantConfiguration?> FindTenantAsync(string tenantIdOrName)
    {
        if (Guid.TryParse(tenantIdOrName, out var parsedTenantId))
        {
            return await TenantStore.FindAsync(parsedTenantId);
        }
        else
        {
            return await TenantStore.FindAsync(TenantNormalizer.NormalizeName(tenantIdOrName)!);
        }
    }
}
