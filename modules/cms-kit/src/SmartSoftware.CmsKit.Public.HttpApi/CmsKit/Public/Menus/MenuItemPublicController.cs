using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Menus;

namespace SmartSoftware.CmsKit.Public.Menus;

[RequiresFeature(CmsKitFeatures.MenuEnable)]
[RequiresGlobalFeature(typeof(MenuFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/menu-items")]
public class MenuItemPublicController : CmsKitPublicControllerBase, IMenuItemPublicAppService
{
    protected IMenuItemPublicAppService MenuPublicAppService { get; }

    public MenuItemPublicController(IMenuItemPublicAppService menuPublicAppService)
    {
        MenuPublicAppService = menuPublicAppService;
    }

    [HttpGet]
    public Task<List<MenuItemDto>> GetListAsync()
    {
        return MenuPublicAppService.GetListAsync();
    }
}
