using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}
