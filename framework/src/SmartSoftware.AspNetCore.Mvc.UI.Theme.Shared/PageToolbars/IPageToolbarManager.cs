using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(string pageName);
}
