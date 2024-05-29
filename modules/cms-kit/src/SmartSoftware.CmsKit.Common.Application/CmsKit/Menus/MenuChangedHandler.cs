using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Caching;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;

namespace SmartSoftware.CmsKit.Menus;

public class MenuChangedHandler :
  ILocalEventHandler<EntityUpdatedEventData<MenuItem>>,
  ILocalEventHandler<EntityDeletedEventData<MenuItem>>,
  ILocalEventHandler<EntityCreatedEventData<MenuItem>>,
  ITransientDependency
{
    protected IDistributedCache<List<MenuItemDto>> DistributedCache { get; }

    public MenuChangedHandler(IDistributedCache<List<MenuItemDto>> distributedCache)
    {
        DistributedCache = distributedCache;
    }

    public Task HandleEventAsync(EntityUpdatedEventData<MenuItem> eventData)
    {
        return DistributedCache.RemoveAsync(MenuApplicationConsts.MainMenuCacheKey);
    }

    public Task HandleEventAsync(EntityDeletedEventData<MenuItem> eventData)
    {
        return DistributedCache.RemoveAsync(MenuApplicationConsts.MainMenuCacheKey);
    }

    public Task HandleEventAsync(EntityCreatedEventData<MenuItem> eventData)
    {
        return DistributedCache.RemoveAsync(MenuApplicationConsts.MainMenuCacheKey);
    }
}
