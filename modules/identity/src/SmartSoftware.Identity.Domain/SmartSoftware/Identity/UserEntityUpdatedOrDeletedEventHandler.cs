using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;
using SmartSoftware.Security.Claims;
using SmartSoftware.Uow;

namespace SmartSoftware.Identity;

public class UserEntityUpdatedOrDeletedEventHandler :
    ILocalEventHandler<EntityUpdatedEventData<IdentityUser>>,
    ILocalEventHandler<EntityDeletedEventData<IdentityUser>>,
    ITransientDependency
{
    public ILogger<UserEntityUpdatedOrDeletedEventHandler> Logger { get; set; }

    private readonly IDistributedCache<SmartSoftwareDynamicClaimCacheItem> _dynamicClaimCache;

    public UserEntityUpdatedOrDeletedEventHandler(IDistributedCache<SmartSoftwareDynamicClaimCacheItem> dynamicClaimCache)
    {
        Logger = NullLogger<UserEntityUpdatedOrDeletedEventHandler>.Instance;

        _dynamicClaimCache = dynamicClaimCache;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(EntityUpdatedEventData<IdentityUser> eventData)
    {
        await RemoveDynamicClaimCacheAsync(eventData.Entity.Id, eventData.Entity.TenantId);
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(EntityDeletedEventData<IdentityUser> eventData)
    {
        await RemoveDynamicClaimCacheAsync(eventData.Entity.Id, eventData.Entity.TenantId);
    }

    protected virtual async Task RemoveDynamicClaimCacheAsync(Guid userId, Guid? tenantId)
    {
        Logger.LogDebug($"Remove dynamic claims cache for user: {userId}");
        await _dynamicClaimCache.RemoveAsync(SmartSoftwareDynamicClaimCacheItem.CalculateCacheKey(userId, tenantId));
    }
}
