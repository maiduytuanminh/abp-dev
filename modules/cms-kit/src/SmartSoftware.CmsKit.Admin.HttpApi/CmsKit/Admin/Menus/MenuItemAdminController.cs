﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Features;
using SmartSoftware.GlobalFeatures;
using SmartSoftware.CmsKit.Features;
using SmartSoftware.CmsKit.GlobalFeatures;
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Permissions;

namespace SmartSoftware.CmsKit.Admin.Menus;

[RequiresFeature(CmsKitFeatures.MenuEnable)]
[RequiresGlobalFeature(typeof(MenuFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitAdminRemoteServiceConsts.ModuleName)]
[Authorize(CmsKitAdminPermissions.Menus.Default)]
[Route("api/cms-kit-admin/menu-items")]
public class MenuItemAdminController : CmsKitAdminController, IMenuItemAdminAppService
{
    protected IMenuItemAdminAppService MenuItemAdminAppService { get; }

    public MenuItemAdminController(IMenuItemAdminAppService menuAdminAppService)
    {
        MenuItemAdminAppService = menuAdminAppService;
    }

    [HttpGet]
    public virtual Task<ListResultDto<MenuItemDto>> GetListAsync()
    {
        return MenuItemAdminAppService.GetListAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<MenuItemWithDetailsDto> GetAsync(Guid id)
    {
        return MenuItemAdminAppService.GetAsync(id);
    }

    [HttpPost]
    [Authorize(CmsKitAdminPermissions.Menus.Create)]
    public virtual Task<MenuItemDto> CreateAsync(MenuItemCreateInput input)
    {
        return MenuItemAdminAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(CmsKitAdminPermissions.Menus.Update)]
    public virtual Task<MenuItemDto> UpdateAsync(Guid id, MenuItemUpdateInput input)
    {
        return MenuItemAdminAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(CmsKitAdminPermissions.Menus.Delete)]
    public virtual Task DeleteAsync(Guid id)
    {
        return MenuItemAdminAppService.DeleteAsync(id);
    }

    [HttpPut]
    [Route("{id}/move")]
    [Authorize(CmsKitAdminPermissions.Menus.Update)]
    public virtual Task MoveMenuItemAsync(Guid id, MenuItemMoveInput input)
    {
        return MenuItemAdminAppService.MoveMenuItemAsync(id, input);
    }

    [HttpGet]
    [Route("lookup/pages")]
    public virtual Task<PagedResultDto<PageLookupDto>> GetPageLookupAsync(PageLookupInputDto input)
    {
        return MenuItemAdminAppService.GetPageLookupAsync(input);
    }
}
