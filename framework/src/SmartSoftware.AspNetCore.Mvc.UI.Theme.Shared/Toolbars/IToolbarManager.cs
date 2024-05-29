using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}
