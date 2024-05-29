using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Entities.Events;
using SmartSoftware.EventBus;
using SmartSoftware.CmsKit.Pages;

namespace SmartSoftware.CmsKit.Menus;

public class PageChangedHandler :
    ILocalEventHandler<EntityUpdatedEventData<Page>>,
    ITransientDependency
{
    protected IMenuItemRepository MenuRepository { get; }
    protected MenuItemManager MenuManager { get; }

    public PageChangedHandler(
        IMenuItemRepository menuRepository,
        MenuItemManager menuManager)
    {
        MenuRepository = menuRepository;
        MenuManager = menuManager;
    }

    public async Task HandleEventAsync(EntityUpdatedEventData<Page> eventData)
    {
        // TODO: Write a repository query.
        var allMenuItems = await MenuRepository.GetListAsync();

        var affectedMenuItems = allMenuItems
                                .Where(x => x.PageId == eventData.Entity.Id)
                                .ToArray();

        if (affectedMenuItems.IsNullOrEmpty())
        {
            return;
        }

        foreach (var menuItem in affectedMenuItems)
        {
            MenuManager.SetPageUrl(menuItem, eventData.Entity);
        }

        await MenuRepository.UpdateManyAsync(affectedMenuItems);
    }
}
