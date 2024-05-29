using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Pages.SmartSoftware.MultiTenancy.ClientProxies;
using SmartSoftware.AspNetCore.Mvc.MultiTenancy;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Threading;
using DependencyAttribute = SmartSoftware.DependencyInjection.DependencyAttribute;

namespace SmartSoftware.AspNetCore.Components.MauiBlazor;

[Dependency(ReplaceServices = true)]
public class MauiBlazorRemoteTenantStore : ITenantStore, ITransientDependency
{
    protected SmartSoftwareTenantClientProxy TenantAppService { get; }
    protected IDistributedCache<TenantConfiguration> Cache { get; }

    public MauiBlazorRemoteTenantStore(SmartSoftwareTenantClientProxy tenantAppService, IDistributedCache<TenantConfiguration> cache)
    {
        TenantAppService = tenantAppService;
        Cache = cache;
    }

    public async Task<TenantConfiguration?> FindAsync(string normalizedName)
    {
        var cacheKey = CreateCacheKey(normalizedName);

        var tenantConfiguration = await Cache.GetOrAddAsync(
            cacheKey,
            async () => CreateTenantConfiguration(await TenantAppService.FindTenantByNameAsync(normalizedName))!,
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5) //TODO: Should be configurable.
            }
        );

        return tenantConfiguration;
    }

    public async Task<TenantConfiguration?> FindAsync(Guid id)
    {
        var cacheKey = CreateCacheKey(id);

        var tenantConfiguration = await Cache.GetOrAddAsync(
            cacheKey,
            async () => CreateTenantConfiguration(await TenantAppService.FindTenantByIdAsync(id))!,
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5) //TODO: Should be configurable.
            }
        );

        return tenantConfiguration;
    }

    public Task<IReadOnlyList<TenantConfiguration>> GetListAsync(bool includeDetails = false)
    {
        return Task.FromResult<IReadOnlyList<TenantConfiguration>>(Array.Empty<TenantConfiguration>());
    }

    public TenantConfiguration? Find(string normalizedName)
    {
        var cacheKey = CreateCacheKey(normalizedName);

        var tenantConfiguration = Cache.GetOrAdd(
            cacheKey,
            () => AsyncHelper.RunSync(async () => CreateTenantConfiguration(await TenantAppService.FindTenantByNameAsync(normalizedName)))!,
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5) //TODO: Should be configurable.
            }
        );

        return tenantConfiguration;
    }

    public TenantConfiguration? Find(Guid id)
    {
        var cacheKey = CreateCacheKey(id);

        var tenantConfiguration = Cache.GetOrAdd(
            cacheKey,
            () => AsyncHelper.RunSync(async () => CreateTenantConfiguration(await TenantAppService.FindTenantByIdAsync(id)))!,
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5) //TODO: Should be configurable.
            }
        );

        return tenantConfiguration;
    }

    protected virtual TenantConfiguration? CreateTenantConfiguration(FindTenantResultDto tenantResultDto)
    {
        if (!tenantResultDto.Success || tenantResultDto.TenantId == null)
        {
            return null;
        }

        return new TenantConfiguration(tenantResultDto.TenantId.Value, tenantResultDto.Name!, tenantResultDto.NormalizedName!);
    }

    protected virtual string CreateCacheKey(string normalizedName)
    {
        return $"RemoteTenantStore_Name_{normalizedName}";
    }

    protected virtual string CreateCacheKey(Guid tenantId)
    {
        return $"RemoteTenantStore_Id_{tenantId:N}";
    }
}
