using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Menus;

namespace SmartSoftware.CmsKit.Public.Menus;

public interface IMenuItemPublicAppService : IApplicationService
{
    Task<List<MenuItemDto>> GetListAsync();
}
