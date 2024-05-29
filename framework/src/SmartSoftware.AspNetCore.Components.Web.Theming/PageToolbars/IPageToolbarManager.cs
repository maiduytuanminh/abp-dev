using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
}
