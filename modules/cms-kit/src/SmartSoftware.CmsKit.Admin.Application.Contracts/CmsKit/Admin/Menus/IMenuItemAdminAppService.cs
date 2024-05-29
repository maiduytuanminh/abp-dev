﻿using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Menus;

namespace SmartSoftware.CmsKit.Admin.Menus;

public interface IMenuItemAdminAppService : IApplicationService
{
    Task<ListResultDto<MenuItemDto>> GetListAsync();

    Task<MenuItemWithDetailsDto> GetAsync(Guid id);

    Task<MenuItemDto> CreateAsync(MenuItemCreateInput input);

    Task<MenuItemDto> UpdateAsync(Guid id, MenuItemUpdateInput input);

    Task DeleteAsync(Guid id);

    Task MoveMenuItemAsync(Guid id, MenuItemMoveInput input);

    Task<PagedResultDto<PageLookupDto>> GetPageLookupAsync(PageLookupInputDto input);
}
